# AnonymousFile

#### Introduction
- Make your file name transparent

#### Sample
```csharp
// Prepare some variables
var fs = default(FileStream);
var id = int.MaxValue;
var file = new AnonymousFile("folder");

// Create file
using(fs = file.Create(id))
using(var sw = new StreamWriter(fs))
    sw.WriteLine("content");


// Retrieve file
if(file.Retrieve(id, out fs))
    using(var sr = new StreamReader(fs))
        System.Diagnostics.Debug(sr.ReadLine());

// Delete file
file.Delete(id);
```

#### License
MIT License