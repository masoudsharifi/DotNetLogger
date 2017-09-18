<h1>DotNetLogger</h1>
<p>This is a library that provides logging for .NET Core 2.0 applications. It gives the option of logging to Disk, SQL database or MongoDb. It also has a find method that can be used to search for logs based on time range, partial message string, log type (Exception, Error, Warning, Information).</p>

<h2>Sample Code</h2>
<p>
<h3>Log to Disk</h3>
logger = new DiskLogger();
LogController controller = new LogController(logger);
controller.LogException(new Exception("My exception 1", new Exception("My inner exception 1")), "MyException");
controller.LogException(new Exception("My exception 2", new Exception("My inner exception 2")), "MyException");
controller.LogException(new Exception("My exception 3", new Exception("My inner exception 3")), "MyException");

DateTime dt0 = new DateTime(2017, 09, 18);
DateTime dt1 = new DateTime(2017, 09, 19);
var logs = controller.FindLogs(dt0, dt1, "exception 1");

<h3>Log to SQL Database</h3>
logger = new SqlLogger(databaseConnectionString);
LogController controller = new LogController(logger);
controller.LogException(new Exception("My exception 1", new Exception("My inner exception 1")), "MyException");
controller.LogException(new Exception("My exception 2", new Exception("My inner exception 2")), "MyException");
controller.LogException(new Exception("My exception 3", new Exception("My inner exception 3")), "MyException");

DateTime dt0 = new DateTime(2017, 09, 18);
DateTime dt1 = new DateTime(2017, 09, 19);
var logs = controller.FindLogs(dt0, dt1, "exception 1");

<h3>Log to MongoDb</h3>
logger = new MongoDbLogger(mongoDatabaseConnection);
LogController controller = new LogController(logger);
controller.LogException(new Exception("My exception 1", new Exception("My inner exception 1")), "MyException");
controller.LogException(new Exception("My exception 2", new Exception("My inner exception 2")), "MyException");
controller.LogException(new Exception("My exception 3", new Exception("My inner exception 3")), "MyException");

DateTime dt0 = new DateTime(2017, 09, 18);
DateTime dt1 = new DateTime(2017, 09, 19);
var logs = controller.FindLogs(dt0, dt1, "exception 1");
</p>