namespace M1APP.utils
{
    /*
  public static class HidePassword : Une classe statique ne peut pas être instanciée.*@
   •	public static string ToAsterisks(this string input) : C'est une méthode d'extension. 
   Les méthodes d'extension permettent d'ajouter des méthodes à des types existants 
   sans les modifier directement.
   Le mot-clé this avant le premier paramètre indique que cette méthode est une méthode d'extension pour le type string.
    */
    public static class HidePassword
    {
        public static string ToAsterisks(this string input)
        {
            return new string('*', input.Length);
        }
    }

   
}
