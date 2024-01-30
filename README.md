This is just a project for fun to learn aspnet 7 and work with an idea about allowing different dbm systems.  

The Models project holds modelling for CRUD and POCO along with Interfaces and shared methods.  (Maybe it should be called shared.)
The DataService project allows you to configure the desired database and sets up Operations implementing the CRUD interface. It also has some logic to create a DB and tables for PostgreSql.
