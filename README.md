# Massive

### Database Setup
I have used SQL Express 2016, but should be fine with any SQL version above 2012 R2.

1. Build and Publish the GraphDB project
  - Name the Database "GraphDB", select the the DB instance and hit publish
2. Update ConnectionString's data source
  - in the App.Config files for projects [GraphLoader's App.config](https://github.com/Zefrock/Massive/blob/master/MassiveSolution/GraphLoader/App.config) and [GraphDB's App.config](https://github.com/Zefrock/Massive/blob/master/MassiveSolution/GraphLib/App.Config), where found `DESKTOP-A7UHQ71\SQLEXPRESS` replace with your own data source name.

### Load Graph from Input Data
The provided samples can be found under source control [Input Data](https://github.com/Zefrock/Massive/tree/master/InputData)
The relative location is set on [GraphLoader's App.config](https://github.com/Zefrock/Massive/blob/master/MassiveSolution/GraphLoader/App.config) configuration file.
Modify `GraphFolder` AppSetting key value to direct it to another location.

### Graph Loader
GraphLoader is a command line app that loads a Graph from xml data files and inserts the graph onto a DB.
Every flawless execution of the loader will reset the previous Graph data from DB.
If no input data is found the graph is reset to 0 nodes.
All adjacent nodes self references are removed from the graph.
The loader will fail if it finds duplicated node definitions *(i.e. two definitions with the same node Id)* in which case any existing graph is preserved in the DB.
