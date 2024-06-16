using System.Data.SQLite;
class Database
{
    //Attributs of the class
    private SQLiteConnection conDb;

    //Construct of the class
    public Database()
    {
        // Connection to the database
        string dbPath = @".\database.db"; //Path of the database 
        string connectionString = $"Data Source={dbPath};Version=3;"; //String connextion of the database 
        this.conDb = new SQLiteConnection(connectionString); //Create the class of the database
    }

    // Get all the childrens of an id given
    public List<(int, int, int)> getChildrens(int id)
    {
        List<(int, int, int)> childrensList = new List<(int, int, int)>();

        //Connection to the database and query
        this.conDb.Open();

        //If the move has no father
        string query;
        if (id == -1) { query = "SELECT id_move, total_game, win_game FROM Move WHERE father_move IS NULL;"; }
        else { query = "SELECT id_move, total_game, win_game FROM Move WHERE father_move = @id;"; }

        using (SQLiteCommand command = new SQLiteCommand(query, this.conDb))
        {
            if (id != -1) { command.Parameters.AddWithValue("@id", id); }
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    childrensList.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                }
            }
        }
        this.conDb.Close();

        return childrensList;
    }

    // Get move of an id
    public ((int, int), (int, int)) getMove(int id)
    {
        ((int, int), (int, int)) move = ((-1, -1), (-1, -1));

        //Connection to the database and query
        this.conDb.Open();

        string query = "SELECT CM.x, CM.y, CW.x, CW.y FROM Move AS M INNER JOIN Coordinate AS CM ON M.id_coordinate = CM.id_coordinate INNER JOIN Wall AS W ON M.id_wall = W.id_wall INNER JOIN Coordinate AS CW ON W.id_coordinate = CW.id_coordinate WHERE M.id_move = @id;";
        using (SQLiteCommand command = new SQLiteCommand(query, this.conDb))
        {
            command.Parameters.AddWithValue("@id", id);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    move = ((reader.GetInt32(0), reader.GetInt32(1)), (reader.GetInt32(2), reader.GetInt32(3)));
                }
            }
        }
        this.conDb.Close();

        return move;
    }

    //Create all the children of a move and a pawn
    public void createChildrensMove(int idFatherMove, List<(int, int)> possibleMove, List<(int, int)> possibleVerticalWall, List<(int, int)> possibleHorizontalWall, int pawn)
    {
        this.conDb.Open();

        foreach ((int, int) move in possibleMove)
        {
            int idCoordinate = -1;
            //Get the id of the coordinate
            string queryCoordinate = "SELECT id_coordinate FROM Coordinate WHERE x = @x AND y = @y;";
            using (SQLiteCommand command = new SQLiteCommand(queryCoordinate, this.conDb))
            {
                command.Parameters.AddWithValue("@x", move.Item1);
                command.Parameters.AddWithValue("@y", move.Item2);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idCoordinate = reader.GetInt32(0);
                    }
                }
            }

            foreach ((int, int) wall in possibleVerticalWall)
            {
                int idWallVertical = -1;
                // Get the id of the vertical wall
                string queryWallVertical = "SELECT id_wall FROM Wall AS W INNER JOIN Coordinate AS C ON W.id_coordinate = C.id_coordinate WHERE C.x = @x AND C.y = @y AND W.direction = 0;";
                using (SQLiteCommand command = new SQLiteCommand(queryWallVertical, this.conDb))
                {
                    command.Parameters.AddWithValue("@x", wall.Item1);
                    command.Parameters.AddWithValue("@y", wall.Item2);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idWallVertical = reader.GetInt32(0);
                        }
                    }
                }

                //Create the children
                string queryCreateChildren = "INSERT INTO Move(pawn, total_game, win_game, father_move, id_wall, id_coordinate) VALUES (@pawn, 0, 0, @father, @wall, @coordinate);";
                SQLiteCommand insertSQL = new SQLiteCommand(queryCreateChildren, this.conDb);
                insertSQL.Parameters.AddWithValue("@pawn", pawn);
                insertSQL.Parameters.AddWithValue("@father", idFatherMove);
                insertSQL.Parameters.AddWithValue("@wall", idWallVertical);
                insertSQL.Parameters.AddWithValue("@coordinate", idCoordinate);
                insertSQL.ExecuteNonQuery();
            }

            foreach ((int, int) wall in possibleHorizontalWall)
            {
                int idWallHorizontal = -1;
                // Get the id of the horizontal wall
                string queryWallHorizontal = "SELECT id_wall FROM Wall AS W INNER JOIN Coordinate AS C ON W.id_coordinate = C.id_coordinate WHERE C.x = @x AND C.y = @y AND W.direction = 1;";
                using (SQLiteCommand command = new SQLiteCommand(queryWallHorizontal, this.conDb))
                {
                    command.Parameters.AddWithValue("@x", wall.Item1);
                    command.Parameters.AddWithValue("@y", wall.Item2);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idWallHorizontal = reader.GetInt32(0);
                        }
                    }
                }

                //Create the children
                string queryCreateChildren = "INSERT INTO Move(pawn, total_game, win_game, father_move, id_wall, id_coordinate) VALUES (@pawn, 0, 0, @father, @wall, @coordinate);";
                SQLiteCommand insertSQL = new SQLiteCommand(queryCreateChildren, this.conDb);
                insertSQL.Parameters.AddWithValue("@pawn", pawn);
                insertSQL.Parameters.AddWithValue("@father", idFatherMove);
                insertSQL.Parameters.AddWithValue("@wall", idWallHorizontal);
                insertSQL.Parameters.AddWithValue("@coordinate", idCoordinate);
                insertSQL.ExecuteNonQuery();
            }

        }
        this.conDb.Close();
    }

    //Save the data of a finish game
    public void saveGame(int[] game)
    {
        this.conDb.Open();
        Console.WriteLine("game = " + game);
        Boolean win = true;
        Console.WriteLine("game length = " + game.Length);

        for (int cmp = game.Length-1; cmp >= 0; cmp--)
        {
            int idMove = game[cmp];
            Console.WriteLine("cmp = " + cmp + " idMove = " + idMove + " win = " + win);
            string queryUpdateData = "";
            if(win)
            {
                queryUpdateData = "UPDATE Move SET total_game = total_game + 1, win_game = win_game + 1 WHERE id_move = @idMove;"; 
                win=!win;
            }
            else
            {
                queryUpdateData = "UPDATE Move SET total_game = total_game + 1 WHERE id_move = @idMove;";
                win=!win;
            }
            SQLiteCommand insertSQL = new SQLiteCommand(queryUpdateData, this.conDb);
            insertSQL.Parameters.AddWithValue("@idMove", idMove);
            insertSQL.ExecuteNonQuery();
        }

        this.conDb.Close();
    }
}
