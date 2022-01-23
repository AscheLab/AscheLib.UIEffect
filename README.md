# AscheLib.UIEffect
Created by Syunta Washidu (AscheLab)

## What's UIEffect?
UIEffect is a library for implementing UI that applies effects to previously rendered graphics

## Install
### Using UnityPackageManager
Find the manifest.json file in the Packages folder of your project and edit it to look like this.
```
"scopedRegistries": [
    {
      "name": "Unofficial Unity Package Manager Registry",
      "url": "https://upm-packages.dev",
      "scopes": [
        "com.aschelab"
      ]
    }
  ],
  "dependencies": {
    "com.aschelab.uieffect": "1.0.1",
  ...
  }
```
## Using for UIEffect
```csharp
using AscheLib.UI
```
1. Create Image
<br>![CreateImage](https://user-images.githubusercontent.com/47095602/72808856-cd4b0800-3c9d-11ea-9e98-3dee60f1e8dc.png)
2. Attach effect component
<br>![AttachEffect](https://user-images.githubusercontent.com/47095602/72810130-4d726d00-3ca0-11ea-880c-f995ee44d7c7.png)
3. Done
<br>![Done](https://user-images.githubusercontent.com/47095602/72808884-db008d80-3c9d-11ea-9f0f-86b2025b096f.png)

## Currently implemented effects
* FrostedGlass
* Grayscale
* InvertColor
* Mosaic
* Sepia
<br>![CurrentlyEffects](https://user-images.githubusercontent.com/47095602/72808903-e2279b80-3c9d-11ea-8baa-af3ba4eac316.png)

## License
This library is under the MIT License.