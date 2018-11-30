module StringReaderProviderTests

open FSharp.Data.StringReaderProvider
open NUnit.Framework

type ExampleFile = StringReaderProvider<"files/example.txt">
type ExampleFileAlt = StringReaderProvider<"files\example.txt">
type MultilineFile = StringReaderProvider<"../test_files/multiline.txt">

[<Test>]
let ``Provider returns correct value`` () =
    Assert.AreEqual(ExampleFile.Content, "Hello world")
    Assert.AreEqual(ExampleFileAlt.Content, "Hello world")
    Assert.AreEqual(MultilineFile.Content, "Something\r\nHere")

type KOI8RFile = StringReaderProvider<"files/example.KOI8-R.txt", "KOI8-R">

[<Test>]
let ``KOI8-R encoding test`` () =
    Assert.AreEqual(KOI8RFile.Content, "Привет")

type Windows1251File = StringReaderProvider<"files/example.windows1251.txt", "windows-1251">

[<Test>]
let ``Windows1251 encoding test`` () =
    Assert.AreEqual(Windows1251File.Content, "Привет мир")