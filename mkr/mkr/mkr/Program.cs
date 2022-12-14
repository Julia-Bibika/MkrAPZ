using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 8 Варіант 
//Ігровий персонаж(назва, кількість НР) може отримувати пошкодження
//різними стихіями магії наприклад: вогнем чи водою.Передбачити
//можливість
//a.Випити еліксир захисту від вогню на X %
//b.Випити еліксир захисту від води на Y%
//c.Отримати захист від всіх видів магії на фіксовану величину.
//d.Відмінити ефект захисту
//Вказати шаблон, який доцільно використати для розв`язування задачі.
namespace mkr
{
    class Program
    {
        static void Main(string[] args)
        {
            // Для цієї задачі доцільно використати структурний шаблон - декоратор 
            IDamage human = new Character("Human", 300);
            human.TakeDamage(5);
            Buff human1 = new Buff(human);
            human1 = new WaterBuff(human, 3, true,100);

        }
    }

    interface IDamage
    {
        void TakeDamage(int Damage);

        bool IsDead();
    }
    class Character : IDamage
    {
        protected int HealthPoints;
        protected string Name;
        public Character(string Name, int HealthPoints)
        {
            this.Name = Name;
            this.HealthPoints = HealthPoints;

        }

        public void TakeDamage(int Damage)
        {
            this.HealthPoints -= Damage;
            Console.WriteLine($"{this.Name} take a hit {Damage}. {this.HealthPoints} health left.Be careful!");
            if (this.IsDead())
                this.Die();
        }

        public bool IsDead()
        {
            return this.HealthPoints <= 0;
        }
        protected void Die()
        {
            Console.WriteLine($"Oh no,{this.Name} is dead!");
        }
        protected void DrinkWaterPoiton()
        {
            Console.WriteLine($"{this.Name} drink a water potion");
        }
        protected void DrinkFirePoiton()
        {
            Console.WriteLine($"{this.Name} drink a fire potion");
        }
    }
    class Buff : IDamage
    {
        protected IDamage Damage;
        public Buff(IDamage Damage)
        {
            this.Damage = Damage;
        }
        public virtual void TakeDamage(int Damage)
        {
            this.Damage.TakeDamage(Damage);
        }
        public bool IsDead()
        {
            return this.Damage.IsDead();
        }
        public IDamage Undecorate()
        {
            return this.Damage;
        }

    }
    class WaterBuff : Buff
    {
        protected int Time;
        protected bool Effect;
        protected double DamageCoeffitient;
        public WaterBuff(IDamage Damage, int DefencePercet,bool effect,int Time) : base(Damage)
        {
            this.DamageCoeffitient = 1 - DefencePercet / 100.0;
            this.Effect = effect;
            this.Time = Time;
        }

        public override void TakeDamage(int Damage)
        {
            base.TakeDamage((int)Math.Floor(Damage * this.DamageCoeffitient));
        }
        public void CancelEffect(int Damage)
        {
            base.TakeDamage(Damage);
            this.Effect = false;
            Console.WriteLine("Ви відмінили ефект захисту");
        }
        public void TimeBuff(int Time,int Damage)
        {
            while(Time > 0)
            {
                Time--;
            }
            if(Time == 0)
            {
                Console.WriteLine("Час дії ефекту закінчився");
                CancelEffect(Damage);
            }
        }
    }
    class FireBuff : Buff
    {
        protected int Time;
        protected bool Effect;
        protected double DamageCoeffitient;
        public FireBuff(IDamage Damage, int DefencePercet, bool effect, int Time) : base(Damage)
        {
            this.DamageCoeffitient = 1 - DefencePercet / 100.0;
            this.Effect = effect;
            this.Time = Time;
        }

        public override void TakeDamage(int Damage)
        {
            base.TakeDamage((int)Math.Floor(Damage * this.DamageCoeffitient));
        }
        public void CancelEffect(int Damage)
        {
            base.TakeDamage(Damage);
            this.Effect = false;
            Console.WriteLine("Ви відмінили ефект захисту");
        }
        public void TimeBuff(int Time, int Damage)
        {
            while (Time > 0)
            {
                Time--;
            }
            if (Time == 0)
            {
                Console.WriteLine("Час дії ефекту закінчився");
                CancelEffect(Damage);
            }
        }
    }
}

   
