


Jestream Xamarin Demo App
===================

SSC Note!
-------------

Install the "Jetstream mobile app package-1.0" first.

Android Google Maps configuration
-------------
To make google maps available on your device when you build Android application from sources you need to perform several steps.

1) [Download](https://github.com/Sitecore/sitecore-jetstream-xamarin-demo/raw/master/scripts/android/debug.keystore) our debug.keystore from repository.

2) In 

```
src/android/JetstreamTablet/Properties/AndroidManifest.xml
```

replace following line:

```
<meta-data android:name="com.google.android.maps.v2.API_KEY" android:value="AIzaSyDCbSr0J9m2pVrVXgEX_Hyw7ro-l8seprk"/>
```

with 

```
<meta-data android:name="com.google.android.maps.v2.API_KEY" android:value="AIzaSyCa3ojwnnEEf4YrTcxhf5LjNL999_6MObA"/>
```

3) Configure project in Xamarin Studio. 

#### For Xamarin Studio:

- Open **JetstreamAndroidTablet.sln** solution from **solutions** folder.
- From **JetstreamTablet** project's context menu in solutions pad select **Options**.  

![OptionsMenu.png](https://raw.githubusercontent.com/Sitecore/sitecore-jetstream-xamarin-demo/master/doc/images/Maps_Configuration/OptionsMenu.png)

- In **Build** menu select **Android Package Signing** item.  

![AndroidPackageSigning.png](https://raw.githubusercontent.com/Sitecore/sitecore-jetstream-xamarin-demo/master/doc/images/Maps_Configuration/AndroidPackageSigning.png)

- Select configuration **Debug**.  
- Tick check box **Sign the .APK file using the following keystore details**.  
- Browse for  **debug.keystore**  downloaded before for **Keystore** field.  
- Fulfill options as on provided screenshot.

![FulfillOptions.png](https://raw.githubusercontent.com/Sitecore/sitecore-jetstream-xamarin-demo/master/doc/images/Maps_Configuration/FulfillOptions.png)

- Press **Ok** button and run project.

##### If you are experiencing some problems with Xamarin Studio you can manually edit 

```
src/android/JetstreamTablet/JetstreamTablet.csproj
```

Add following lines: 

```
<AndroidKeyStore>True</AndroidKeyStore>
<AndroidSigningKeyStore>path\to\downloaded\debug.keystore</AndroidSigningKeyStore>
<AndroidSigningStorePass>android</AndroidSigningStorePass>
<AndroidSigningKeyAlias>androiddebugkey</AndroidSigningKeyAlias>
<AndroidSigningKeyPass>android</AndroidSigningKeyPass>
```

at the end of section:

```
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug</OutputPath>
    <DefineConstants>DEBUG;LOGGING</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <ConsolePause>false</ConsolePause>
    <EmbedAssembliesIntoApk>True</EmbedAssembliesIntoApk>
    <AndroidSupportedAbis>armeabi;armeabi-v7a;x86;arm64-v8a;x86_64</AndroidSupportedAbis>
    <Debugger>Xamarin</Debugger>
    <DevInstrumentationEnabled>True</DevInstrumentationEnabled>
<!-- here -->
  </PropertyGroup>
```
