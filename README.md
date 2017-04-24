# Massive

## Database Setup
I have used SQL Express 2016, but should be fine with any SQL version above 2012 R2.

1. Build and Publish the GraphDB project
  - Name the Database "GraphDB", select the the DB instance and hit publish
2. Update ConnectionString's data source
  - in the App.Config files for projects `GraphLoader` and `GraphDB`, where found `DESKTOP-A7UHQ71\SQLEXPRESS` replace with your own data source name.
