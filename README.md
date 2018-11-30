## String reader provider

F# Type Provider which provides file content as a string constant

### Example

```
type ExampleFile = StringReaderProvider<"files/example.txt", (optional) Encoding="UTF-8">
```

### Building:

    .paket\paket.exe update

    dotnet build -c release

    .paket\paket.exe pack \output --version 0.0.1