using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace neo4j
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"),"neo4j","dba");
            client.Connect();
            Console.WriteLine("Connected successfully");
           
            //var tathemQuery = client.Cypher.Start("root", client.RootNode).Match("root-[:HAS_USER]->user")
            //     .Where((User user) => user.Name == "Tatham").Return<Node<User>>("user");
            //var tatham = tathemQuery.Results.Single();
            //var movies = client.Cypher.Match("(m:Movie)").Return(m=>m.As<Movie>()).Limit(10).Results.ToList<Movie>();
            //foreach(Movie movie in movies)
            //{
            //    Console.WriteLine("{0} ({1}) - {2}", movie.Title, movie.Released, movie.TagLine);
            //}
            //Console.ReadLine();

            User salem = new User{ Name = "Salem" };
            Console.WriteLine(salem.Name);
            //client.Cypher.Create("(user:User {salem})").WithParam("salem", salem).ExecuteWithoutResults();
            //client.Cypher.Match("(user1:User)", "(user2:User)").Where((User user1) => user1.Name == "Amr").AndWhere((User user2) => user2.Name == "Salem").Create("(user1)-[:HAS_FATHER]->(user2)").ExecuteWithoutResults();
            var res = client.Cypher.Match("(son)-[:HAS_FATHER]->(father)-[:HAS_FATHER]->(grandfather)").Return((son,grandfather) => new { Son=son.As<User>(),Grandfather=grandfather.As<User>() }).Results;
            foreach(var user in res)
            {
                Console.WriteLine(user.Son.Name);
                Console.WriteLine(user.Grandfather.Name);
            }
            Console.WriteLine(res.Count());
            Console.ReadLine();


        }
    }
    public class Movie
    {
        [JsonProperty(PropertyName="title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "released")]
        public string Released { get; set; }
        [JsonProperty(PropertyName = "tagline")]
        public string TagLine { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
    }
    public class Activity
    {
        public string Name { get; set; }
    }
}
