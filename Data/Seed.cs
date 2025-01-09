using Labb4_MVC_Razer.Data;
using Labb4_MVC_Razer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Labb4_MVC_Razor.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            Console.WriteLine("SeedData method is being executed.");

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.Migrate();

                SeedBooks(context);
            }
        }

        private static void SeedBooks(ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                // Skönlitteratur
                new Book
                {
                    BookName = "Mitt sanna jag",
                    Author = "Tara Westover",
                    Genre = "Skönlitteratur",
                    BookDescription = "En gripande självbiografisk roman om en kvinnas resa från en isolerad barndom i en mormonfamilj till akademisk framgång och personlig frigörelse.",
                    ReleaseYear = new DateTime(2018, 2, 20),
                    InStock = true,
                    ISBN = "9781501161307"
                },
                new Book
                {
                    BookName = "The Goldfinch",
                    Author = "Donna Tartt",
                    Genre = "Skönlitteratur",
                    BookDescription = "En fängslande berättelse om en ung pojke som överlever en katastrof på ett konstmuseum och som genom en unik tavla får en komplex väg genom livet.",
                    ReleaseYear = new DateTime(2013, 10, 22),
                    InStock = true,
                    ISBN = "9780316055437"
                },
                new Book
                {
                    BookName = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = "Skönlitteratur",
                    BookDescription = "A novel set in the American South during the 1930s, focusing on the Finch family, with particular emphasis on the trial and conviction of an innocent black man accused of raping a white woman.",
                    ReleaseYear = new DateTime(1960, 7, 11),
                    InStock = true,
                    ISBN = "9780061120084"
                },

                // Deckare
                new Book
                {
                    BookName = "Män som hatar kvinnor",
                    Author = "Stieg Larsson",
                    Genre = "Deckare",
                    BookDescription = "Den första boken i Millennium-trilogin introducerar läsarna till journalisten Mikael Blomkvist och hackaren Lisbeth Salander när de samarbetar för att lösa ett mystiskt försvinnande. Fylld med spänning och intriger, är detta en modern klassiker inom deckargenren.",
                    ReleaseYear = new DateTime(2005, 8, 23),
                    InStock = true,
                    ISBN = "9780307269751"
                },
                new Book
                {
                    BookName = "Gone Girl",
                    Author = "Gillian Flynn",
                    Genre = "Deckare",
                    BookDescription = "En psykologisk thriller om en kvinna som försvinner på sin femte bröllopsdag och de mörka hemligheter som avslöjas när hennes man blir misstänkt för hennes försvinnande.",
                    ReleaseYear = new DateTime(2012, 6, 5),
                    InStock = true,
                    ISBN = "9780307588371"
                },
                new Book
                {
                    BookName = "Sherlock Holmes: A Study in Scarlet",
                    Author = "Arthur Conan Doyle",
                    Genre = "Deckare",
                    BookDescription = "Den första boken som introducerar läsarna till den legendariska detektiven Sherlock Holmes och hans trogna följeslagare, Dr. John Watson. Tillsammans löser de en mordgåta som involverar en hemlig sekt och en amerikansk koloni i Utah.",
                    ReleaseYear = new DateTime(1887, 11, 1),
                    InStock = true,
                    ISBN = "9780192123167"
                },
                new Book
                {
                    BookName = "Människans Lott",
                    Author = "Malin Persson Giolito",
                    Genre = "Deckare",
                    BookDescription = "En spännande deckare som kastar ljus över mörka hemligheter och familjedramer. När en ung kvinna mördas, avslöjar utredningen skrämmande sanningar som hotar att förstöra flera liv.",
                    ReleaseYear = new DateTime(2020, 10, 1),
                    InStock = true,
                    ISBN = "9789177952816"
                },

                // Feelgood
                new Book
                {
                    BookName = "En man som heter Ove",
                    Author = "Fredrik Backman",
                    Genre = "Feelgood",
                    BookDescription = "En hjärtevärmande berättelse om en grinig gammal man som finner oväntad vänskap och mening i livet när han möter sina nya grannar. Med humor och värme fångar Backman läsarna och påminner dem om vikten av medmänsklighet och försoning.",
                    ReleaseYear = new DateTime(2012, 8, 27),
                    InStock = true,
                    ISBN = "9789170370499"
                },
                new Book
                {
                    BookName = "The Rosie Project",
                    Author = "Graeme Simsion",
                    Genre = "Feelgood",
                    BookDescription = "En charmig roman om en professor i genetik som designar en vetenskaplig undersökning för att hitta sin perfekta match, men som hittar kärleken där han minst anar det.",
                    ReleaseYear = new DateTime(2013, 10, 1),
                    InStock = true,
                    ISBN = "9781443414259"
                },
                new Book
                {
                    BookName = "Brev från Klara",
                    Author = "Jessica Gedin",
                    Genre = "Feelgood",
                    BookDescription = "En charmig och gripande berättelse om kärlek, vänskap och det magiska med att skriva och få brev. Följ med Klara när hon utforskar livets alla nyanser genom sina brev och upptäcker den verkliga kraften i kommunikation.",
                    ReleaseYear = new DateTime(2020, 3, 16),
                    InStock = true,
                    ISBN = "9789189366218"
                },
                new Book
                {
                    BookName = "Den lilla bokhandeln runt hörnet",
                    Author = "Jenny Colgan",
                    Genre = "Feelgood",
                    BookDescription = "En underbar feelgood-roman om kärlek, vänskap och gemenskap som utspelar sig i en liten bokhandel. Följ med Nina när hon kämpar för att rädda sin bokhandel och hittar kärleken på vägen.",
                    ReleaseYear = new DateTime(2016, 3, 1),
                    InStock = true,
                    ISBN = "9789176291898"
                },

                // Fantasy
                new Book
                {
                    BookName = "Harry Potter och De Vises Sten",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "Den första boken i Harry Potter-serien tar läsarna med på en magisk resa till Hogwarts skola för häxkonster och trolldom. Följ med Harry Potter när han upptäcker sin sanna arv och står inför den mörka trollkarlen som dödade hans föräldrar.",
                    ReleaseYear = new DateTime(1997, 6, 26),
                    InStock = true,
                    ISBN = "9789129699390",
                    ImageUrl = "https://bilder.akademibokhandeln.se/images_akb/9789129723946_383/harry-potter-och-de-vises-sten"
                },
                new Book
                {
                    BookName = "Harry Potter och Hemligheternas Kammare",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "Den andra boken i Harry Potter-serien där Harry återvänder till Hogwarts för sitt andra år och ställs inför nya mysterier och faror när hemligheter från det förflutna kommer i dagen.",
                    ReleaseYear = new DateTime(1998, 7, 2),
                    InStock = true,
                    ISBN = "9789129677367",
                    ImageUrl="https://bilder.akademibokhandeln.se/images_akb/9789129723960_383/harry-potter-och-hemligheternas-kammare"
                },
                new Book
                {
                    BookName = "Harry Potter och Fången från Azkaban",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "I den tredje boken i serien får Harry veta mer om sin egen historia och konfronterar en farlig fånge som rymt från det ökända fängelset Azkaban.",
                    ReleaseYear = new DateTime(1999, 7, 8),
                    InStock = true,
                    ISBN = "9789129677374"
                },
                new Book
                {
                    BookName = "Harry Potter och Den Flammande Bägaren",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "Den fjärde boken i Harry Potter-serien där Harry deltar i den farliga Tretrollkarlsturneringen och möter både fysiska och emotionella utmaningar.",
                    ReleaseYear = new DateTime(2000, 7, 8),
                    InStock = true,
                    ISBN = "9789129677398"
                },
                new Book
                {
                    BookName = "Harry Potter och Fenixorden",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "I den femte boken får Harry och hans vänner ta itu med den mörka kraften som hotar världen, samtidigt som de hanterar både interna och externa konflikter.",
                    ReleaseYear = new DateTime(2003, 6, 21),
                    InStock = true,
                    ISBN = "9789129677398"
                },
                new Book
                {
                    BookName = "Harry Potter och Halvblodsprinsen",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "Den sjätte boken i serien där Harry lär sig mer om Lord Voldemorts bakgrund och förbereder sig för den stora konfrontationen som närmar sig.",
                    ReleaseYear = new DateTime(2005, 7, 16),
                    InStock = true,
                    ISBN = "9789129697692"
                },
                new Book
                {
                    BookName = "Harry Potter och Dödsrelikerna",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    BookDescription = "Den avslutande boken i Harry Potter-serien, där Harry, Ron och Hermione söker efter de sista delarna av Voldemorts själ för att besegra honom en gång för alla.",
                    ReleaseYear = new DateTime(2007, 7, 21),
                    InStock = true,
                    ISBN = "9789129697692"
                },

                // Science Fiction
                new Book
                {
                    BookName = "Dune",
                    Author = "Frank Herbert",
                    Genre = "Science Fiction",
                    BookDescription = "En episk science fiction-roman som utspelar sig på planeten Arrakis, där Paul Atreides måste navigera i en värld av politiska intriger och mystiska krafter för att uppfylla sitt öde.",
                    ReleaseYear = new DateTime(1965, 8, 1),
                    InStock = true,
                    ISBN = "9780441013593"
                },
                new Book
                {
                    BookName = "Neuromancer",
                    Author = "William Gibson",
                    Genre = "Science Fiction",
                    BookDescription = "En banbrytande cyberpunk-roman som följer en före detta datasnikare när han går med på att utföra ett uppdrag i en värld fylld av digitala mysterier och cyberkrig.",
                    ReleaseYear = new DateTime(1984, 7, 1),
                    InStock = true,
                    ISBN = "9780441569595"
                },
                new Book
                {
                    BookName = "Foundation",
                    Author = "Isaac Asimov",
                    Genre = "Science Fiction",
                    BookDescription = "Den första boken i Asimovs klassiska Foundation-serie som handlar om en grupp forskare som försöker bevara mänsklig kunskap och kultur genom att etablera en ny civilisation i en tid av kommande mörker.",
                    ReleaseYear = new DateTime(1951, 6, 1),
                    InStock = true,
                    ISBN = "9780553293357"
                },
                new Book
                {
                    BookName = "The Left Hand of Darkness",
                    Author = "Ursula K. Le Guin",
                    Genre = "Science Fiction",
                    BookDescription = "En innovativ science fiction-roman som utforskar teman av kön och identitet genom en diplomat som besöker en planet där invånarna inte har fasta könsroller.",
                    ReleaseYear = new DateTime(1969, 3, 1),
                    InStock = true,
                    ISBN = "9780441013593"
                },

                // Historisk roman
                new Book
                {
                    BookName = "All the Light We Cannot See",
                    Author = "Anthony Doerr",
                    Genre = "Historisk roman",
                    BookDescription = "En rörande och vackert skriven berättelse som följer en blind fransk flicka och en tysk pojke under andra världskriget när deras liv korsas i det krigshärjade Europa.",
                    ReleaseYear = new DateTime(2014, 5, 6),
                    InStock = true,
                    ISBN = "9781501173218"
                },
                new Book
                {
                    BookName = "The Book Thief",
                    Author = "Markus Zusak",
                    Genre = "Historisk roman",
                    BookDescription = "En gripande roman som utspelar sig i Nazityskland, där en ung flicka stjäl böcker och delar dem med andra, medan hon navigerar genom krigets tragedier.",
                    ReleaseYear = new DateTime(2005, 3, 14),
                    InStock = true,
                    ISBN = "9780375842207"
                },
                new Book
                {
                    BookName = "The Nightingale",
                    Author = "Kristin Hannah",
                    Genre = "Historisk roman",
                    BookDescription = "En fängslande berättelse om två systrar i det ockuperade Frankrike under andra världskriget och deras mod och motstånd mot nazisterna.",
                    ReleaseYear = new DateTime(2015, 2, 3),
                    InStock = true,
                    ISBN = "9780312577223"
                },

                // Rysare
                new Book
                {
                    BookName = "It",
                    Author = "Stephen King",
                    Genre = "Rysare",
                    BookDescription = "En skräckroman som handlar om en grupp barn som möter en ondskefull kraft som tar formen av en clown och terroriserar deras hemstad. När de växer upp och återvänder, måste de konfrontera sina rädslor och stoppa det onda en gång för alla.",
                    ReleaseYear = new DateTime(1986, 9, 15),
                    InStock = true,
                    ISBN = "9780452284290"
                },
                new Book
                {
                    BookName = "The Haunting of Hill House",
                    Author = "Shirley Jackson",
                    Genre = "Rysare",
                    BookDescription = "En skrämmande och psykologiskt komplex roman om fyra personer som samlas på en gammal herrgård med ett rykte om att vara hemsökt.",
                    ReleaseYear = new DateTime(1959, 10, 16),
                    InStock = true,
                    ISBN = "9780143039983"
                },
                new Book
                {
                    BookName = "Psycho",
                    Author = "Robert Bloch",
                    Genre = "Rysare",
                    BookDescription = "En skräckklassiker om Norman Bates, en ung man som driver ett ensamt motell och bär på mörka hemligheter. När en kvinna försvinner efter att ha checkat in, avslöjas chockerande sanningar.",
                    ReleaseYear = new DateTime(1959, 5, 1),
                    InStock = true,
                    ISBN = "9780312924585"
                },
                new Book
                {
                    BookName = "Dracula",
                    Author = "Bram Stoker",
                    Genre = "Rysare",
                    BookDescription = "En klassisk skräckroman som berättar historien om greve Dracula och hans försök att flytta till England för att sprida sin vampyrism. Denna bok är en av de mest kända och inflytelserika inom genren.",
                    ReleaseYear = new DateTime(1897, 5, 26),
                    InStock = true,
                    ISBN = "9780486288094"
                },

                // Romantik
                new Book
                {
                    BookName = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Genre = "Romantik",
                    BookDescription = "En tidlös roman som skildrar romantiska intriger och sociala normer i det tidiga 1800-talets England. Följ Elizabeth Bennet och Mr. Darcy när de navigerar kärlekens komplexiteter och missförstånd.",
                    ReleaseYear = new DateTime(1813, 1, 28),
                    InStock = true,
                    ISBN = "9780141439518"
                },
                new Book
                {
                    BookName = "Me Before You",
                    Author = "Jojo Moyes",
                    Genre = "Romantik",
                    BookDescription = "En hjärtskärande kärlekshistoria om en ung kvinna som blir vårdgivare för en förlamad man och hur deras liv förändras när de utvecklar en oväntad relation.",
                    ReleaseYear = new DateTime(2012, 1, 5),
                    InStock = true,
                    ISBN = "9780141048180"
                },
                new Book
                {
                    BookName = "The Notebook",
                    Author = "Nicholas Sparks",
                    Genre = "Romantik",
                    BookDescription = "En rörande kärlekshistoria som följer Noah och Allie, vars kärlek trotsar tidens prövningar och familjehinder. Deras berättelse är både gripande och inspirerande.",
                    ReleaseYear = new DateTime(1996, 10, 1),
                    InStock = true,
                    ISBN = "9781455512850"
                },
                new Book
                {
                    BookName = "The Rosie Project",
                    Author = "Graeme Simsion",
                    Genre = "Romantik",
                    BookDescription = "En charmig roman om en professor i genetik som designar en vetenskaplig undersökning för att hitta sin perfekta match, men som hittar kärleken där han minst anar det.",
                    ReleaseYear = new DateTime(2013, 10, 1),
                    InStock = true,
                    ISBN = "9781443414259"
                }
            };
                try
                {
                    context.Books.AddRange(books);
                    context.SaveChanges();
                    Console.WriteLine("Books have been successfully seeded.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding the books: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Books table already contains data.");
            }
        }
    }
}