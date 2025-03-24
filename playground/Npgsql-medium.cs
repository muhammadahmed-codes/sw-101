using Npgsql;

class ConfigData {
    required public string Host { get; set; }
    required public int Port { get; set; }
    required public string Username { get; set; }
    required public string Password { get; set; }
    required public string Database { get; set; }
}

class UserSchema {
    required public string Username { get; set; }
    required public string Email { get; set; }
    required public string Password { get; set; }
    required public string Phone_Number { get; set; }
    required public string Privilege { get; set; }
}

class Database {   
    public NpgsqlConnection connection;

    public Database(ConfigData config) {
        string ConnectionString = $"Host={config?.Host};Port={config?.Port};Username={config?.Username};Password={config?.Password};Database={config?.Database}";
        connection = new NpgsqlConnection(ConnectionString);
    }

    public void CreateTable() {
        string sql = "CREATE TABLE IF NOT EXISTS users (username TEXT, email TEXT, password TEXT, phone_number TEXT, privilege TEXT)";
        
        try {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table created successfully.");
            }
        }
        catch (Exception e) {
            Console.WriteLine("Error creating table: " + e.Message);
        }
        finally {
            connection.Close();
        }
    }

    public void WriteData(UserSchema userdata) {
        string sql = "INSERT INTO users (username, email, password, phone_number, privilege) VALUES (:USERNAME, :EMAIL, :PASSWORD, :PHONE_NUMBER, :PRIVILEGE)";
        try {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("USERNAME", userdata.Username);
                cmd.Parameters.AddWithValue("EMAIL", userdata.Email);
                cmd.Parameters.AddWithValue("PASSWORD", userdata.Password);
                cmd.Parameters.AddWithValue("PHONE_NUMBER", userdata.Phone_Number);
                cmd.Parameters.AddWithValue("PRIVILEGE", userdata.Privilege);

                int rowsAdded = cmd.ExecuteNonQuery();

                if (rowsAdded == 0) {
                    Console.WriteLine("Could not add user.");
                }
            } 
        }
        catch(Exception E) {
            Console.WriteLine("Exception: " + E);
        }
        finally {
            connection.Close();
        }
    }

    public void UpdateData(UserSchema userdata) {
        string sql = "UPDATE users SET privilege = @PRIVILEGE WHERE username = @USERNAME";

        try {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@PRIVILEGE", userdata.Privilege);
                cmd.Parameters.AddWithValue("@USERNAME", userdata.Username);

                int rowsUpdated = cmd.ExecuteNonQuery();

                if (rowsUpdated == 0) {
                    Console.WriteLine("Could not update user's data");
                }
            } 
        } catch (Exception E) {
            Console.WriteLine("Exception: " + E);
        } finally {
            connection.Close();
        }
    }

    public void DeleteData(UserSchema userdata) {
        string sql = "DELETE FROM users WHERE username = @USERNAME";

        try {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@USERNAME", userdata.Username);

                int rowsUpdated = cmd.ExecuteNonQuery();

                if (rowsUpdated == 0) {
                    Console.WriteLine("Could not delete user's data.");
                } else {
                    Console.WriteLine("Data Deleted.");
                }
            } 
        } catch (Exception E) {
            Console.WriteLine("Exception: " + E);
        } finally {
            connection.Close();
        }
    }

    public void ReadData() {
        string sql = "SELECT * FROM users";
        NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);

        connection.Open();
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read()) {
                string username = reader.GetString(reader.GetOrdinal("username"));
                string email = reader.GetString(reader.GetOrdinal("email"));
                string password = reader.GetString(reader.GetOrdinal("password"));
                string phone_number = reader.GetString(reader.GetOrdinal("phone_number"));
                string privilege = reader.GetString(reader.GetOrdinal("privilege"));
                Console.WriteLine("---------------------------");
                Console.WriteLine(username);
                Console.WriteLine(email);
                Console.WriteLine(password);
                Console.WriteLine(phone_number);
                Console.WriteLine(privilege);
            }
        }
        connection.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConfigData config = new() {
            Host = "localhost", 
            Port = 5432, 
            Username = "postgres", 
            Password = "Manutd1604:*", 
            Database = "gamenest"
        };

        Database database = new(config);
        
        UserSchema userdata = new() {
            Username = "UserName23",
            Email = "muhammad.ahmed@nu.edu.pk",
            Password = "DoNotCopy",
            Phone_Number = "03042145648",
            Privilege = "moderator"
        };

        database.CreateTable();

        database.WriteData(userdata);
        database.UpdateData(
            new UserSchema {
                Username = "UserName23",
                Email = "muhammad.ahmed@nu.edu.pk",
                Password = "DoNotCopy",
                Phone_Number = "03042145648",
                // From "moderator" to "superadmin"
                Privilege = "superadmin"
            }
        );
        database.ReadData();
        // database.DeleteData(
        //     new UserSchema {
        //         Username = "UserName23",
        //         Email = "muhammad.ahmed@nu.edu.pk",
        //         Password = "DoNotCopy",
        //         Phone_Number = "03042145648",
        //         // From "moderator" to "superadmin"
        //         Privilege = "superadmin"
        //     }
        // );
        // database.ReadData();
    }
}