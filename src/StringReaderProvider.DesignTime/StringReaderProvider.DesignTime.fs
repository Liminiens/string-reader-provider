module StringReaderProviderImplementation

open System
open System.IO
open System.Reflection
open FSharp.Quotations
open FSharp.Core.CompilerServices
open FSharp.Data.StringReaderProvider
open ProviderImplementation
open ProviderImplementation.ProvidedTypes
open System.Text

[<TypeProvider>]
type StringReaderProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces (config, assemblyReplacementMap=[("StringReaderProvider.DesignTime", "StringReaderProvider.Runtime")])     

    let ns = "FSharp.Data.StringReaderProvider"
    let asm = Assembly.GetExecutingAssembly()

    do asm.Location |> Path.GetDirectoryName |> this.RegisterProbingFolder
    // check we contain a copy of runtime files, and are not referencing the runtime DLL
    do assert (typeof<``Asm marker``>.Assembly.GetName().Name = asm.GetName().Name)  

    let getRelativePath path = 
        let replaceAltChars (str: string) =         
            match Environment.OSVersion.Platform with
            | PlatformID.Unix | PlatformID.MacOSX ->
                str.Replace('\\', Path.DirectorySeparatorChar)
            | _ ->
                str.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
        Path.GetFullPath(Path.Combine(config.ResolutionFolder, replaceAltChars path))

    let createType typeName (args: obj array) =
        EncodingUtility.registerCodePages()

        let asm = ProvidedAssembly()
        let path = args.[0] :?> string
        let encodingName = args.[1] :?> string
        let tpType = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, isErased=false)

        let filePath =
            match Path.IsPathRooted(path) with
            | false -> getRelativePath path
            | true -> path

        if not <| File.Exists(filePath) then
            failwithf "Specified file \"%s\" could not be found" path

        let content = File.ReadAllText(filePath, Encoding.GetEncoding(encodingName)).Trim()

        let contentField = ProvidedField.Literal("Content", typeof<string>, content)
        contentField.AddXmlDoc(sprintf "Content of '%s'" filePath)
        tpType.AddMember(contentField)

        tpType

    let staticParameters = 
        [ProvidedStaticParameter("Path", typeof<string>);
         ProvidedStaticParameter("Encoding", typeof<string>, "UTF-8")]

    let summaryText = 
        """<summary>String reader type provider</summary>
           <param name='Path'>Path to file</param>
           <param name='Encoding'>File encoding, default is 'UTF-8'</param>"""

    let generatedType = 
        let t = ProvidedTypeDefinition(asm, ns, "StringReaderProvider", Some typeof<obj>, isErased=false)
        t.DefineStaticParameters(staticParameters, fun typeName args -> createType typeName args)
        t.AddXmlDoc(summaryText)
        t
    do
        this.AddNamespace(ns, [generatedType])


[<TypeProviderAssembly>]
do ()
