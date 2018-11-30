module StringReaderProviderTests


open MyNamespace
open NUnit.Framework

[<Test>]
let ``Default constructor should create instance`` () =
    Assert.AreEqual("My internal state", MyType().InnerState)

