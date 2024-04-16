using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PICardPlayer
{
    internal class Card
    {
        public string name;
        public int healthPoint;
        public int originAttackPoint;
        public int attackPoint;
        public double offensivePoint;
        public double defenceMultiplier;
        public int type;// 0:living; 1:dead; 2:micro;
        public List<Skill> skills;
        public Card(string name, int healthPoint, double firstHand, int type, string skills)
        {
            this.name = name;
            this.healthPoint = healthPoint;
            this.offensivePoint = firstHand;
            this.originAttackPoint = (int)Math.Pow(firstHand, 4d);
            this.attackPoint = this.originAttackPoint;
            this.type = type;
            this.defenceMultiplier = 0;
            this.skills = [];
            string[] skillCodeList = skills.Split([',']);
            foreach (string id in skillCodeList)
            {
                Skill skill = EquipmentFactoryManager.CreateSkill(id.Trim());
                AddSkill(skill);
            }
        }
        private void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }
        public void Attacked(Card other)
        {
            int damage;
            if ((type == 1 && other.type == 0) || (type == 2 && other.type == 1) || (type == 0 && other.type == 2))
            {
                damage = (int)(other.attackPoint * 2 * (1 - defenceMultiplier)); // dominated
                Console.WriteLine($"{name} was dominated by {other.name}!");
            }
            else
                damage = (int)(other.attackPoint * (1 - defenceMultiplier)); // undominated
            Console.WriteLine($"{name} was attacked, whose health point reduced by {damage}!");
            healthPoint -= damage;
            if (healthPoint > 0) Console.WriteLine($"Now {name}'s health point is {healthPoint}!");
            else Console.WriteLine($"{name} got killed! {other.name} won the game!");
        }
        public void PrintProperties()
        {
            Console.WriteLine($"[Name: {name}]");
            Console.WriteLine($"HealthPoint: {healthPoint}");
            Console.WriteLine($"OffensivePoint: {offensivePoint}");
            Console.WriteLine($"AttackPoint: {originAttackPoint}");
            string typeString = type switch
            {
                0 => "Living",
                1 => "Dead",
                2 => "Micro",
                _ => "No Type"
            }; ;
            Console.WriteLine($"Type: {typeString}");
            Console.WriteLine();
        }
    }
}
