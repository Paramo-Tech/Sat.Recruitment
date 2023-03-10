**********************************************************************************
TO RESCAFFOLD ENTITIES FROM THE LOCAL DB INTO THE PERSISTENCE PROJECT
**********************************************************************************

Scaffold-DbContext "Data Source=(localdb)\MSSQLLocalDB;Database=SatRecruitmentDb;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -Force