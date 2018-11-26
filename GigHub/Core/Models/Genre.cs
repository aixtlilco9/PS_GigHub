using System.ComponentModel.DataAnnotations;

//used data-annotations to override code-first conventions
//did so to over ride nullibility in Name for Genre and remove the max length that originally was
//name is no longer max and allows for nulls
namespace GigHub.Core.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        public string Name { get; set; }

    }
}