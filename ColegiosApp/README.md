# ColegiosApp
Este Backend está creado usando .NET6, una API Rest y la conexión a base de datos se hizo utilizando EntityFramework.
Para crear la base de datos en su maquina local tiene que modificar el (connection string) de nombre DbContext localizado en el archivo appsettings.json en el proyecto ColegioAPI con el valor de la ruta de su base de datos local, una vez modificado este valor tiene que dirigirse a la consola administrador de paquetes, apuntar hacia el proyecto ColegioData y ejecutar el comando Update-database para ejecutar todas las migraciones pendientes.
