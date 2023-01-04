using Microsoft.Data.SqlClient;

using var conn = new SqlConnection("Server=localhost;Database=Task6.SQL;TrustServerCertificate=True;Integrated Security=True;");
conn.Open();

void SaveFromStorage(SqlConnection connection, string filename, string tableName, string saveTo)
{
    var sql = $"SELECT file_stream FROM {tableName} WHERE name = '{filename}'";
    var cmd  = new SqlCommand(sql, connection);
    using var rdr = cmd.ExecuteReader();
    rdr.Read();
    var data = rdr.GetSqlBinary(0).Value;
    File.WriteAllBytes(saveTo, data);
}

SaveFromStorage(conn, "2.txt", "DocumentsStore", @"C:\Temp\DB\1111.txt");