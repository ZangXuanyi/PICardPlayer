namespace PICardPlayer
{
    internal interface ISkillFactory
    {
        Skill CreateSkill();
    }
    internal class EquipmentFactoryManager
    {
        private static readonly Dictionary<string, ISkillFactory> Factories = [];
        public static void RegisterFactory(string id, ISkillFactory factory)
        {
            Factories[id] = factory;
        }
        public static Skill CreateSkill(string id)
        {
            if (Factories.TryGetValue(id, out ISkillFactory factory))
            {
                return factory.CreateSkill();
            }
            throw new InvalidOperationException($"UnRegistered ID: {id}");
        }
        public static void RegisterFactories()
        {
            RegisterFactory("A1001", new CellWallFactory());
            RegisterFactory("A1002", new ViolentMutationFactory());
            RegisterFactory("A1003", new RequirementArmorFactory());
            RegisterFactory("A1004", new YellowBubbleFactory());
            RegisterFactory("A1005", new VaccineThornFactory());
            RegisterFactory("A1007", new PerquisiteFactory());
            RegisterFactory("A1008", new RollingCompactionFactory());
            RegisterFactory("A1009", new LightingTimeFactory());
            RegisterFactory("A1010", new ClassicPlayerFactory());   
            RegisterFactory("A1011", new TotalEnhancement108Factory());
            RegisterFactory("A1012", new EmperorOfEraIFactory());
            RegisterFactory("A1016", new TotalEnhancement318Factory());
            RegisterFactory("A2002", new ALSFactory());
            //there will be more...
        }
    }
    internal abstract class Skill
    {
        public static string? name;
        public static string? id;
    }
    internal class SkillA : Skill
    {
        public virtual void Trigger(Card card)
        {
            Console.WriteLine("This is a default skill and has no effect.");
        }
    }
    internal class SkillA1 : SkillA;
    internal class SkillA2 : SkillA;
    internal class CellWall : SkillA1
    {
        public CellWall()
        {
            name = "CellWall";
            id = "A1001";
        }
        public override void Trigger(Card card)
        {
            card.healthPoint = (int)(1.1 * card.healthPoint);
            card.defenceMultiplier += 0.1;
            Console.WriteLine($"{card.name} used [CellWall], " +
                $"healthpoint reached to {card.healthPoint}, " +
                $"defenceMultiplier reached to {card.defenceMultiplier}!");
        }
    }
    internal class CellWallFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new CellWall();
        }
    }
    internal class ViolentMutation : SkillA1
    {
        public ViolentMutation()
        {
            name = "ViolentMutation";
            id = "A1002";
        }
        public override void Trigger(Card card)
        {
            card.attackPoint = (int)(1.2 * (double)card.attackPoint);
            Console.WriteLine($"{card.name} used [ViolentMutation], " +
                $"attackPoint reached to {card.attackPoint}!");
        }
    }
    internal class ViolentMutationFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new ViolentMutation();
        }
    }
    internal class RequirementArmor : SkillA1
    {
        public RequirementArmor()
        {
            name = "RequirementArmor";
            id = "A1003";
        }
        public override void Trigger(Card card)
        {
            card.healthPoint = (int)(1.2 * card.healthPoint);
            Console.WriteLine($"{card.name} used [RequirementArmor], " +
                $"health point reached to {card.healthPoint}!");
        }
    }
    internal class RequirementArmorFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new RequirementArmor();
        }
    }
    internal class YellowBubble : SkillA1
    {
        public YellowBubble()
        {
            name = "YellowBubble";
            id = "A1004";
        }
        public override void Trigger(Card card)
        {
            card.offensivePoint += 0.5;
            card.attackPoint = (int)(Math.Pow(card.attackPoint, 4d));
            Console.WriteLine($"{card.name} used [YellowBubble], " +
                $"offensive point reached to {card.offensivePoint}, " +
                $"attack point reached to {card.attackPoint}!");
        }
    }
    internal class YellowBubbleFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new YellowBubble();
        }
    }
    internal class VaccineThorn : SkillA1
    {
        public VaccineThorn()
        {
            name = "VaccineThorn";
            id = "A1005";
        }
        public override void Trigger(Card card)
        {
        Loop:
            Console.WriteLine($"Please enter the num of {card.name}'s vaccine(int):");
            int? n = int.Parse(Console.ReadLine());
            if (n == null || n <= 0)
            {
                Console.WriteLine("No valid input! Please input again!");
                goto Loop;
            }
            card.defenceMultiplier += 0.05 * (double)n <= 0.4 ? 0.05 * (double)n : 0.4;
            Console.WriteLine($"{card.name} used [VaccineThorn], " +
                $"defense multiplier reached to {card.defenceMultiplier}!");
        }
    }
    internal class VaccineThornFactory() : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new VaccineThorn();
        }
    }
    internal class Perquisite : SkillA1
    {
        public Perquisite()
        {
            name = "Perquisite";
            id = "A1007";
        }
        public override void Trigger(Card card)
        {
        Loop:
            Console.WriteLine($"Please enter the num of the Festival scenarios(int):");
            int? n = int.Parse(Console.ReadLine());
            if (n == null || n <= 0)
            {
                Console.WriteLine("No valid input! Please input again!");
                goto Loop;
            }
            card.healthPoint = (int)(card.healthPoint *(1+ (0.2 * (double)n <= 0.8 ? 0.2 * (double)n : 0.8)));
            Console.WriteLine($"{card.name} used [Perquisite], " +
                $"healthPoint reached to {card.healthPoint}!");
        }
    }
    internal class PerquisiteFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new Perquisite();
        }
    }
    internal class RollingCompaction : SkillA1
    {
        public RollingCompaction()
        {
            name = "RollingCompaction";
            id = "A1008";
        }
        public override void Trigger(Card card)
        {
            card.healthPoint = (int)(1.2 * card.healthPoint);
            Console.WriteLine($"{card.name} used [RollingCompaction], " +
                $"health point reached to {card.healthPoint}!");
        }
    }
    internal class RollingCompactionFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new RollingCompaction();
        }
    }
    internal class LightingTime : SkillA1
    {
        public LightingTime()
        {
            name = "LightingTime";
            id = "A1009";
        }
        public override void Trigger(Card card)
        {
            card.offensivePoint += 0.3;
            card.attackPoint = (int)(Math.Pow(card.attackPoint, 4d));
            Console.WriteLine($"{card.name} used [LightingTime], " +
                $"offensive point reached to {card.offensivePoint}, " +
                $"attack point reached to {card.attackPoint}!");
        }
    }
    internal class LightingTimeFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new LightingTime();
        }
    }
    internal class ClassicPlayer : SkillA1
    {
        public ClassicPlayer()
        {
            name = "ClassicPlayer";
            id = "A1010";
        }
        public override void Trigger(Card card)
        {
            card.attackPoint = (int)(1.2 * card.attackPoint);
            Console.WriteLine($"{card.name} used [ClassicPlayer], " +
                $"attackPoint reached to {card.attackPoint}!");
        }
    }
    internal class ClassicPlayerFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new ClassicPlayer();
        }
    }
    internal class TotalEnhancement108 : SkillA1
    {
        public TotalEnhancement108()
        {
            name = "TotalEnhancement108";
            id = "A1011";
        }
        public override void Trigger(Card card)
        {
            card.healthPoint = (int)(1.4 * card.healthPoint);
            card.offensivePoint += 0.3;
            card.attackPoint = (int)Math.Pow(card.attackPoint, 4d);
            card.attackPoint *= (int)(1.2 * card.attackPoint);
            Console.WriteLine($"{card.name} used [TotalEnhancement108], " +
                $"offensive point reached to {card.offensivePoint}, " +
                $"attack point reached to {card.attackPoint}, " +
                $"healthPoint reached to {card.healthPoint}!");
        }
    }
    internal class TotalEnhancement108Factory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new TotalEnhancement108();
        }
    }
    internal class EmperorOfEraI : SkillA1
    {
        public EmperorOfEraI()
        {
            name = "EmperorOfEraI";
            id = "A1012";
        }
        public override void Trigger(Card card)
        {
        Loop:
            Console.WriteLine($"Please enter the num of the Era1 scenarios(int):");
            int? n = int.Parse(Console.ReadLine());
            if (n == null || n <= 0)
            {
                Console.WriteLine("No valid input! Please input again!");
                goto Loop;
            }
            card.healthPoint = (int)(card.healthPoint * (1 + (0.05 * (double)n <= 0.15 ? 0.05 * (double)n : 0.15)));
            card.offensivePoint += 0.2 * (double)n <= 0.06 ? 0.2 * (double)n : 0.06;
            card.attackPoint = (int)Math.Pow(card.attackPoint, 4d);
            Console.WriteLine($"{card.name} affected by [EmperorOfEraI], " +
                $"offensive point reached to {card.offensivePoint}, " +
                $"attack point reached to {card.attackPoint}, " +
                $"healthPoint reached to {card.healthPoint}!");
        }
    }
    internal class EmperorOfEraIFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new EmperorOfEraI();
        }
    }
    internal class TotalEnhancement318 : SkillA1
    {
        public TotalEnhancement318()
        {
            name = "TotalEnhancement318";
            id = "A1016";
        }
        public override void Trigger(Card card)
        {
            card.healthPoint = (int)(1.2 * card.healthPoint);
            card.offensivePoint += 0.3;
            card.attackPoint = (int)Math.Pow(card.attackPoint, 4d);
            card.attackPoint *= (int)(1.1 * card.attackPoint);
            Console.WriteLine($"{card.name} used [TotalEnhancement318], " +
                $"offensive point reached to {card.offensivePoint}, " +
                $"attack point reached to {card.attackPoint}, " +
                $"healthPoint reached to {card.healthPoint}!");
        }
    }
    internal class TotalEnhancement318Factory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new TotalEnhancement318();
        }
    }
    internal class ALS : SkillA2
    {
        public ALS()
        {
            name = "ALS";
            id = "A2002";
        }
        public override void Trigger(Card card)
        {
            card.offensivePoint -= 0.5;
            Console.WriteLine($"{card.name} was attacked by [ALS], " +
                $"offensive point reduced to {card.offensivePoint}!");
        }
    }
    internal class ALSFactory : ISkillFactory
    {
        public Skill CreateSkill()
        {
            return new ALS();
        }
    }
}
// Something to say:
// 当增加一个新的技能的时候，需要增加三段代码：
// 1. 接口，也就是Factory，5行；
// 2. 对象，也就是技能本身；
// 3. 工厂注册，也就是FactoryManager，1行。