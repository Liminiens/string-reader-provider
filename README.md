## String reader provider

F# Type Provider which provides file content as a string constant

### Example

```
type ExampleFile = StringReaderProvider<"files/example.txt", (optional) Encoding="UTF-8">
```

## Status

| OS      | Build & Test |
|---------|--------------|
| Mac OS  | [![Build Status](https://dev.azure.com/GithubProjects/StringReaderProvider/_apis/build/status/Liminiens.string-reader-provider)](https://dev.azure.com/GithubProjects/StringReaderProvider/_build/latest?definitionId=2&branchName=master&jobname=macOS_10_13) |
| Linux   | [![Build Status](https://dev.azure.com/GithubProjects/StringReaderProvider/_apis/build/status/Liminiens.string-reader-provider)](https://dev.azure.com/GithubProjects/StringReaderProvider/_build/latest?definitionId=2&branchName=master&jobname=ubuntu_16_04) |
| Windows | [![Build Status](https://dev.azure.com/GithubProjects/StringReaderProvider/_apis/build/status/Liminiens.string-reader-provider)](https://dev.azure.com/GithubProjects/StringReaderProvider/_build/latest?definitionId=2&branchName=master&jobname=vs2017_win2016) |


### Building:

    .paket\paket.exe update

    dotnet build -c release

    .paket\paket.exe pack \output --version 0.0.1