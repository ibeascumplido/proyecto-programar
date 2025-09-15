using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Ranking : IComparable<Ranking>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }

    public Ranking(int id, string nombre, int score, DateTime fecha)
    {
        this.Id = id;
        this.Name = nombre;
        this.Score = score;
        this.Date = fecha;
    }

    public int CompareTo(Ranking other)
    {
        // añadimos condiciones para ordenar la tabla
		// de mayor a menor y de más antiguo a menos antiguo
        if (other.Score > this.Score)
        {
            return 1;
        }
        else if (other.Score < this.Score)
        {
            return -1;
        }
        else if (other.Date > this.Date)
        {
            return -1;
        }
        else if (other.Date < this.Date)
        {
            return 1;
        }
        return 0;

      
    }
}
