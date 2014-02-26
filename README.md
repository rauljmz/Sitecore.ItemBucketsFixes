Item bucket fixes
=================

It adds the ability to specify different folder creation in different Buckets. The different alternatives for folder creation are defined in /sitecore/system/Settings/Buckets/Folder Creators

In that location you just create items that contain reference to a class implementing the IDynamicFolderPath interface.
I provide two examples for GuidBased and DateBased. You can also pass parameters to the classes adding them to the Parameters Field, which then get assigned to public properties. E.g.

|Field  | Value |
|-------|-------|
|Type   |Sitecore.ItemBuckets.DynamicFolderPathResolvers.GuidBasedDynamicFolderResolver,Sitecore.ItemBuckets|
|Parametres|Depth=3|
 
```c#
public class GuidBasedDynamicFolderResolver : IDynamicFolderResolver
{
   public string Depth { get; set; }
...
```

As shown above, GuidBased provides a Depth parametre, DateBased provides
|Parametre| Default|
|-------|-------|
|Format |yyyy/MM/dd/HH/mm|
|Field | __Created |



All items now have an extra field in the Standard Template, within the Buckets section, where you can defined which of the different folder creation schemes should be used.


###Coming next...

URL resolution