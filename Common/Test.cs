using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Common
{
    public class Test
    {
        public static int Sum(int num1, int num2) => num1 + num2;


        public static int ReturnResult(Func<int, int, int> func)
        {
            return func(1, 2);

        }


        public static T2 Cul<T1,T2>(IEnumerable<T1> source,Func<T1,T2,T2> action)
        {
            T2 sum = default(T2);
            foreach(T1 item in source)
            {
               sum= action(item, sum);
            }
            return sum;
        }

    }
    public interface ICC<T>
    {
        string show(T item);
    }
    public class CC : ICC<Animal>
    {
        public string show(Animal t)
        {
           return t.Call();
        }
    }
    public abstract class Animal
    {
        public Animal() : base()
        {

        }
        public virtual void Action() { }
        public string Call()
        {
            return "hello";
        }
    }

    public class Dog : Animal
    {
        public override void Action()
        {
            base.Action();
        }
        public new string Call()
        {
            return "world";
        }
    }
    public class FX<T>
    {
        T _t;
        public FX(T t)
        {
            _t = t;
        }
        public T Log()
        {
            T value = _t;
            return value;
        }
    }
}
