namespace FSharp.Data.StringReaderProvider

type ``Asm marker`` = class end

module EncodingUtility =
    open System.Text
    open System.Threading

    let registerCodePages = 
        #if !NET45
        let mutable isSet = 0
        fun () ->
            if Interlocked.CompareExchange(&isSet, 1, 0) = 0 then
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        #else
        fun () -> ()
        #endif

// Put the TypeProviderAssemblyAttribute in the runtime DLL, pointing to the design-time DLL
[<assembly:CompilerServices.TypeProviderAssembly("StringReaderProvider.DesignTime.dll")>]
do ()
