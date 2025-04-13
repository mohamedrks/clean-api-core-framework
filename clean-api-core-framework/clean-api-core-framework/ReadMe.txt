﻿1. Handle any file uploads through a common handler, it could be large or small as image,
 	a. Check for file name sanitization (regx), extension restrictions, max file size, virus scan
 	b. Read the file as stream to avoid memory overlaod
 	c. Write the file to the temp file in the local disk or in the cloud as aws s3 bucket or azure blobl storage () 
 	d. Read the file from temp storage as stream , to do this, pass the temp path through the method to the method which writes the file to the database
 	e. Now re write it as stream to the database to avoid database over load , finally delete the temp file
 	f. make sure database table has fields to store fileName, MIME , extension and the file stream (bytes)
 	g. store only 100 MB file inthe blob as maximum
 	h. CDN is the best option for iamge storage , may be s3 bucket or find the best option , consider the s3 security in this scenario
 		A. Access Control: Use signed URLs or temporary access tokens if images should not be publicly accessible.
 		B. HTTPS: Serve images over HTTPS to ensure they are securely delivered.
 	i. Implement caching for image reading, mos tof the time it's less liekly to change and is a very good item to cache
 	j. it's better if we can write the way to upload a file to pause it and resume it, this is a very good option for large files
 
 2. Add Load Balancer
 
 3. Add YAML File and deploy via yaml
 
 4. Expand this to K8s
 
 5. Add CI/CD with Azure DevOps
 
 6. Add SonarQube to check code coverage
 
 7. Check do we need to add documentation to each model and attributes ? or only to add complex properties of the class ?
 
 8. Add pagination to GetAll
 
 9. Add logging - SeriLog or any other
 
 10. Add API gateway -
 
 11. Do all the testings - Load est , stress test , spike test , soak test.


 12. Protect the database string for local development 

     Make sure project has     <UserSecretsId>7177a941-12db-49dc-b946-19*****c4a73</UserSecretsId> (Not sure about this )

     Move the current path on terminal to the project folder - C:\Users\user\source\repos\clean-api-core-framework\clean-api-core-framework\clean-api-core-framework
     
     Run the command to init the secret - dotnet user-secrets init
     
     Run the command to add the secret - dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=CleanApiDb;User Id=sa;Password=yourStrong(!)Password;"
     
     In My case - dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS; Database=AlphaDairy; Trusted_Connection=true; Trust Server Certificate=true; MultipleActiveResultSets=true; Integrated 	Security=true;"
     
     Run the command to get the secret - dotnet user-secrets list
     
     Run the command to remove the secret - dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
     
     Run the command to remove all the secrets - dotnet user-secrets clear


     Path where it's stored locally:
        For reference, the secrets are stored as a JSON file in:

        Windows: %APPDATA%\Microsoft\UserSecrets\<userSecretsId>\secrets.json

        Linux/macOS: ~/.microsoft/usersecrets/<userSecretsId>/secrets.json

