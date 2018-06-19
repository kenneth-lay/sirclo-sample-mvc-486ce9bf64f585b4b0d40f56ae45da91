# sirclo-sample-mvc-486ce9bf64f585b4b0d40f56ae45da91

Need to setup Visual Stuido Express & SQL Server Express first

Then set up the db table using table.sql inside the folder

Also need to config the web.config accordingly, especially this part:

```
    <add name="weightlogEntities" connectionString="metadata=res://*/Models.WeightListModel.csdl|res://*/Models.WeightListModel.ssdl|res://*/Models.WeightListModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=KENNETH-THINK\SQLEXPRESS;initial catalog=weightlog;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
```
