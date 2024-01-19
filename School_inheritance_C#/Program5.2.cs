using System;
using System.Collections.Generic;

namespace OLIO_OHJELMOINTI
{
    public class Program
    {
        static void Main()
        {   
            // tehdään lista objekteille, jotta saadan tulostettua objekti
            List<Object> people = new List<Object>();
            bool play = true;

            while (play)
            {
                Console.WriteLine("Haluatko lisätä opettajan(1)\noppilaan(2)\nvaihto-oppilaan(3)\nLopeta painammalla q:ta, tulostamme listan lopuksi");
                string? answer = Console.ReadLine();

                if (answer != null && answer.Equals("q"))
                {
                    play = false;
                }
                // kysytään nimeä, jos käyttäjä valitsi validin numeron
                if (answer != string.Empty && answer != null)
                {   
                    
                    if (int.TryParse(answer, out int newAnswer))
                    {   
                        if(newAnswer>=1 && newAnswer<=3)
                        { 
                            Console.WriteLine("Anna nimi: ");
                            string? name = Console.ReadLine();

                            if (name != null && name != string.Empty)
                            {
                                name.ToString();
                            }
                            else
                            {
                                Console.WriteLine("Syötit tyhjän");
                                play = false;
                            }

                            Console.WriteLine("Anna ikä: ");
                            string? ageString = Console.ReadLine();

                            if (string.IsNullOrEmpty(ageString) || !int.TryParse(ageString, out int age1) || age1 <= 10 || age1 >= 100)
                            {
                                Console.WriteLine("Syötä kelvollinen ikä!");
                                play = false;
                            }
                            //jos ikä on numero tallennetaan se
                            if (int.TryParse(ageString, out int age))
                            {
                                Person person = new Person(name, age);
                            }
                            //ykkös valinnalla lisätään opettaja
                            if (newAnswer == 1)
                            {
                                Console.WriteLine("Palkka: ");
                                string? wage = Console.ReadLine();

                                if (wage != null && wage != string.Empty)
                                {
                                    if (double.TryParse(wage, out double newWage))
                                    {   //palkan täytyy olla positiivinen, jotta voidaan lisätä opettaja
                                        if (newWage >= 0)
                                        {
                                            Teacher teacher = new Teacher(name, age, newWage);
                                            people.Add(teacher);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Palkkasi ei voi olla miinuksella");
                                            play = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Et antanut numeroita");
                                        play = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Syötit tyhjän");
                                    play = false;
                                }
                            
                            }//molemmat sekä vaihtoppilas että oppilas pitää saada vastaamaan kysymykseen tutkinnosta
                            else if (newAnswer == 2 || newAnswer == 3)
                            {   
                                Console.WriteLine("Tutkinto: ");
                                string? fieldOfStudy = Console.ReadLine();
                                if(fieldOfStudy!=string.Empty && fieldOfStudy!=null)
                                {    //jos valinta oli kaksi lisätään oppilas
                                    if(newAnswer == 2)
                                    {   Student oppilas = new Student(name, age, fieldOfStudy);
                                        people.Add(oppilas);
                                    }
                                    else if (newAnswer == 3)
                                    {   //jos valinta oli kolme, lisätään vaihto-oppilas
                                        Console.WriteLine("Kansalaisuus: ");
                                        string? citizenry = Console.ReadLine();
                                        if(citizenry!= null && citizenry!=string.Empty)
                                        {
                                            ExchangeStudent exchangeStudent = new ExchangeStudent(name, age, fieldOfStudy, citizenry);
                                            people.Add(exchangeStudent);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Syötit tyhjän!");
                                            play = false;
                                        }    
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Syötit tyhjän");
                                    play = false;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("numeron täytyy olla 1-3 väliltä");
                        }
    
                    }
                    else
                    {
                        Console.WriteLine("et syöttänyt numeroa!");
                    }

                }
                else
                {
                    Console.WriteLine("Syötit tyhjän");
                }
            }
            foreach (var obj in people)
            {   //tulostetaan lista
                if (obj is Teacher)
                {
                    Console.WriteLine($"Opettaja: {obj}");
                }
                else if (obj is ExchangeStudent)
                {
                    Console.WriteLine($"Vaihto-oppilas: {obj}");
                }
                else if (obj is Student)
                {
                    Console.WriteLine($"Oppilas: {obj}");
                }

            }
  
        }
    }
}