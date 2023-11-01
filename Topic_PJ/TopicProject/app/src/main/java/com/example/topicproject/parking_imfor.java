package com.example.topicproject;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class parking_imfor extends AppCompatActivity
{


    TextView timeshow,moneyshow;
    Button buttontime,buttonmoney;
    String result;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_parking_imfor);

        buttontime =findViewById(R.id.Buttomtime);
        buttonmoney = findViewById(R.id.ButtonMoney);
        timeshow = findViewById(R.id.timeshow);
        moneyshow = findViewById(R.id.moneyshow);

        buttontime.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Thread thread = new Thread(mutiThread);
                thread.start();
            }
        });

        buttonmoney.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Thread thread = new Thread(mThread);
                thread.start();
            }
        });

    }

    private Runnable mutiThread = new Runnable()
    {
        @Override
        public void run()
        {
            try
            {
                URL url = new URL("http://120.117.8.36/android/datacatch.php");
                HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                connection.setRequestMethod("POST");
                connection.setDoOutput(true);
                connection.setDoInput(true);
                connection.setUseCaches(false);
                connection.connect();

                int responseCode = connection.getResponseCode();

                if(responseCode == HttpURLConnection.HTTP_OK)
                {
                    InputStream inputStream = connection.getInputStream();
                    BufferedReader bufReader = new BufferedReader(new InputStreamReader(inputStream, "utf-8"), 8);

                    String line = null;
                    while ((line = bufReader.readLine()) !=null)
                    {
                        JSONArray dataJson = new JSONArray(line);
                        int i = dataJson.length()-1;
                        JSONObject info = dataJson.getJSONObject(i);
                        String time = info.getString("ParkingTimeTotal");

                        result = time.toString();
                    }
                    inputStream.close();

                }

            } catch (Exception e)
            {
                result = e.toString();
            }

            runOnUiThread(new Runnable()
            {
                @Override
                public void run()
                {
                    timeshow.setText("總停車時間"+result+"分鐘");
                }
            });
        }
    };

    private Runnable mThread = new Runnable()
    {
        @Override
        public void run()
        {
            try
            {
                URL url = new URL("http://120.117.8.36/android/getdata.php");
                HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                connection.setRequestMethod("POST");
                connection.setDoOutput(true);
                connection.setDoInput(true);
                connection.setUseCaches(false);
                connection.connect();

                int responseCode = connection.getResponseCode();

                if(responseCode == HttpURLConnection.HTTP_OK)
                {
                    InputStream inputStream = connection.getInputStream();
                    BufferedReader bufReader = new BufferedReader(new InputStreamReader(inputStream, "utf-8"), 8);

                    String line = null;
                    while ((line = bufReader.readLine()) !=null)
                    {
                        JSONArray dataJson = new JSONArray(line);
                        int i = dataJson.length()-1;
                        JSONObject info = dataJson.getJSONObject(i);
                        String money = info.getString("ParkingFee");

                        result = money.toString();
                    }
                    inputStream.close();

                }

            } catch (Exception e)
            {
                result = e.toString();
            }

            runOnUiThread(new Runnable()
            {
                @Override
                public void run()
                {
                    moneyshow.setText("總共累積"+result+"元");
                }
            });
        }
    };
}



