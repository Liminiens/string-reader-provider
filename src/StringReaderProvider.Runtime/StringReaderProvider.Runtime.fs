namespace MyNamespace

open System

// Put any utilities here
[<AutoOpen>]
type ``Asm marker`` = class end

// Put the TypeProviderAssemblyAttribute in the runtime DLL, pointing to the design-time DLL
[<assembly:CompilerServices.TypeProviderAssembly("StringReaderProvider.DesignTime.dll")>]
do ()
