Item bucket fixes
=================

It adds the ability to specify different folder creation in different Buckets. The different alternatives for folder creation are defined in /sitecore/system/Settings/Buckets/Folder Creators
In that location you just create items that contain reference to a class implementing the IDynamicFolderPath interface.
Buckets now have an extra field in the Standard Template, within the Buckets section, where you can defined which of the different folder creation schemes should be used.

Coming next...
URL resolution