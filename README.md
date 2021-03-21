# TestingAzureFunctions
[EN]
Repo to provide an example of how to do test for azure functions.

The test will run with no problems, but, to make the functions work, you will need to add a "local.settings.json" file under the "TestingAzureFunctions.Fnt" project.

![image](https://user-images.githubusercontent.com/39009262/111910241-d762c500-8a60-11eb-8f0a-0856504e47b2.png)

This file will need to have this structure, replacing the values into brackets with your own storage values.

{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
  },
  "AppSettings": {
    "BlobStorageSettings": {
      "StorageConnectionString": "[connection-string]",
      "ContainerName": "[container-name]",
      "BlobName": "[blob-name]"
    }
  }
}

Being: 
[connection-string]: An azure strorage connection string.
[container-name]: The name of a blob container in your storage account.
[blob-name]: The name of a blob (with his file extension ex: "doc.txt") into your blob container.

[ES]
Repositorio que provee un ejemplo de como testear azure functions.

Los test funcionaran sin problemas, pero para hacer que las fucntions funcionen necesitaremos añadir un archivo "local.settings.json", debajo del proyecto de "TestingAzureFunctions.Fnt".

![image](https://user-images.githubusercontent.com/39009262/111910241-d762c500-8a60-11eb-8f0a-0856504e47b2.png)

Este archivo necesariamente tendrá esta estructura, reemplazando los valores entre corchetes con tus propias claves de almacenamiento.

{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
  },
  "AppSettings": {
    "BlobStorageSettings": {
      "StorageConnectionString": "[connection-string]",
      "ContainerName": "[container-name]",
      "BlobName": "[blob-name]"
    }
  }
}

Siendo:
[connection-string]: Una cadena de conexiona tu cuenta de almacenamiento de azure.
[container-name]: El nombre del contenedor de blobs dentro de tu cuenta de almacenamiento.
[blob-name]: El nombre de un blob (con su extensión ej. "doc.txt") dentro del contenedor.
