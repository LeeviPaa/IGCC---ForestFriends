//using UnityEngine;
//using System.Collections;

//    public struct table_t
//    {
//        public int key;
//        public int data;
//        public table_t(int p1,int p2)
//        {
//            key = p1;
//            data = p2;
//        }
//    }
//public class Table
//{
//    /**
//     * テーブルを作成する。
//     * @param table [in] テーブル
//     * @param n [in] データ数
//     */
//    public void CreateTable(table_t[] table, int n)
//    {
//        for (int i = 0; i < n; i++)
//        {
//            table[i].key = new System.Random().Next();
//            table[i].data = new System.Random().Next();
//        }
//    }

//    /**
//     * ダンプする。
//     * @param table [in] テーブル
//     * @param n [in] データ数
//     */
//    public void DumpTable(table_t[] table, int n)
//    {
//        for (int i = 0; i < n; i++)
//        {
//            string key = table[i].key.ToString();
//            string data = table[i].data.ToString();
//            Debug.Log("key = " + key);
//            Debug.Log("data = " + data);
//        }

//        //printf("\n");
//    }

//    public void BubbleSort(table_t[] table, int n)
//    {
//        int s, e, temp;

//        for (s = 1; s < n; s++)
//            for (e = n - 1; e >= s; e--)
//                if (table[e - 1].data > table[e].data)
//                {
//                    temp = table[e].data;
//                    table[e].data = table[e - 1].data;
//                    table[e - 1].data = temp;
//                }
//    }

//    public void InsertSort(table_t[] table, int n)
//    {
//        int s, e, tmp;
//        for (s = 1; s < table.Length; s++)
//        {
//            tmp = table[s].data;
//            if (table[s - 1].data > tmp)
//            {

//                for (e = s; e > 0 && table[e - 1].data > tmp; e--)
//                    table[e].data = table[e - 1].data;

//                table[e].data = tmp;
//            }
//        }
//    }
//}