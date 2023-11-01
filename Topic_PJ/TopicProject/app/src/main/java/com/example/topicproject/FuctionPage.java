package com.example.topicproject;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.View;
import android.widget.Button;

public class FuctionPage extends AppCompatActivity {

     Button camera,Pay;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fuction_page);

        camera = (Button) findViewById(R.id.ButtonScan);
        Pay = (Button) findViewById(R.id.ButtonPay);

        camera.setOnClickListener(new Button.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent intent = new Intent();
                intent.setClass(FuctionPage.this,Camera.class);
                startActivity(intent);
                FuctionPage.this.finish();

            }
        });

      Pay.setOnClickListener(new Button.OnClickListener()
      {
          @Override
          public void onClick(View v)
          {
              Intent x = new Intent(FuctionPage.this,parking_imfor.class);
              startActivity(x);

          }
      });

    }
}