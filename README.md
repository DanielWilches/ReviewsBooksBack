Para ejecutar este Proyecto
•	Descargar el repo 
•	Limpian y luego compilan la solución 
•	Ejecutan los siguientes comandos para migrar el modelo de base de datos

dotnet ef migrations add migracio1 --project Books.InterfaceAdapter.Layer --startup-project Books
dotnet ef database update --project Books.InterfaceAdapter.Layer --startup-project Books
