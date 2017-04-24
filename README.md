# Massive

- Database Setup
I have used SQL Express 2016, but should be fine with any SQL version above 2012 R2.
To setup the database Build and Publish the GraphDB project,
Name the Database "GraphDB", select the the DB instance and hit publish
Update the db instance on the connection string in the App.Config files for projects "GraphLoader" and "GraphDB", where found "DESKTOP-A7UHQ71\SQLEXPRESS;" replace with your own data source.
