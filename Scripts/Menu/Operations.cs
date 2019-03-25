//plik stworzony z metod, ktore moga byc stosowane w wielu przypadkach kilka razy w kilku roznych miejscach. 
//Zamiast powielac ta sama metode stworzylem plik w ktorym trzymam metode, aby nie powielac tej metody
//Tylko sie do nich odwoluje

public class Operations{
    internal float Change_result(float value1, float value2)
    { //przydatne dla wielu obliczen np dopasowanie paska expa admirala, wartosci statku, surowce
        
        float result = Calculate_details(value1, value2);
        if (result > 1f)
        {
            return 1f;
        }
        return result;
    }
    internal float Calculate_details(float value_detail, float max_detail)
    {//dzielenie dwoch wartosci
        return value_detail / max_detail;
    }

    internal int Spent_resources(int metal, int crystal, int deuter)
    {
        return metal + crystal + deuter;
    }

}

