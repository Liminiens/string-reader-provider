namespace FSharp.Data.StringReaderProvider

open System
open System.Text

// Put any utilities here
[<AutoOpen>]
type ``Asm marker`` = class end

module EncodingUtility =
    let registerCodePages() = 
        #if !NET45
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        #else
        ()
        #endif

// Put the TypeProviderAssemblyAttribute in the runtime DLL, pointing to the design-time DLL
[<assembly:CompilerServices.TypeProviderAssembly("StringReaderProvider.DesignTime.dll")>]
do ()
