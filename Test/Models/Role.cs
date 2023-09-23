namespace Test.Models {

    [SQLite.Table("role")]
    public class Role {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        [SQLite.Column("name")]
        public string Name { get; set; }
    }

}
