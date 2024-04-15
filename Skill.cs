using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PICardPlayer
{
    internal interface ISkillFactory
    {
        Skill CreateSkill(string id);
    }
    internal class EquipmentFactoryManager
    {
        private static readonly Dictionary<string, ISkillFactory> Factories = new Dictionary<string, ISkillFactory>();
        public static void RegisterFactory(string id, ISkillFactory factory)
        {
            Factories[id] = factory;
        }
        public static Skill CreateSkill(string id)
        {
            if (Factories.TryGetValue(id, out ISkillFactory factory))
            {
                return factory.CreateSkill(id);
            }
            throw new InvalidOperationException($"UnRegistered ID: {id}");
        }
        public static void RegisterFactories()
        {
            RegisterFactory("A1001", new CellWallFactory());
            RegisterFactory("A1002", new ViolentMutationFactory());
            RegisterFactory("A1003", new RequirementArmorFactory());
            //there will be more...
        }
    }
    internal abstract class Skill { public static string? skillName; public static string? skillID; }
    internal class SkillA : Skill { public virtual void Trigger(Card card) { Console.WriteLine("This is a default skill and has no effect."); } }
    internal class CellWall : SkillA { public override void Trigger(Card card) { card.healthPoint = (int)(1.1 * card.healthPoint); card.defenceMultiplier = 0.1; } }
    internal class CellWallFactory : ISkillFactory { public Skill CreateSkill(string id) { return new CellWall(); } }
    internal class ViolentMutation : SkillA { public override void Trigger(Card card) { card.actualAttackPoint *= (int)(1.1 * card.actualAttackPoint); } }
    internal class ViolentMutationFactory : ISkillFactory { public Skill CreateSkill(string id) { return new ViolentMutation(); } }
    internal class RequirementArmor : SkillA { public override void Trigger(Card card) { card.healthPoint = (int)(1.2 * card.healthPoint); } }
    internal class RequirementArmorFactory : ISkillFactory { public Skill CreateSkill(string id) { return new RequirementArmor(); } }
}
// Something to say:
// 当增加一个新的技能的时候，需要增加三段代码：
// 1. 接口，也就是Factory，5行；
// 2. 对象，也就是技能本身；
// 3. 工厂注册，也就是FactoryManager，1行。