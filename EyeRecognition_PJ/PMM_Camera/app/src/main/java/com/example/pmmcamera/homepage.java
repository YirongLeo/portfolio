package com.example.pmmcamera;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;

public class homepage extends AppCompatActivity
{

    LinearLayout layout_camera, layout_image, layout_connect;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_homepage);

        layout_camera = findViewById(R.id.layoutphoto);
        layout_image = findViewById(R.id.layoutImage);
        layout_connect = findViewById(R.id.layoutConnect);

        //開啟相機
        layout_camera.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View view)
            {

                Intent intent = new Intent();
                intent.setClass(homepage.this,camera.class);
                startActivity(intent);

//                Intent intent = new Intent(); //呼叫照相機
//                intent.setAction("android.media.action.STILL_IMAGE_CAMERA");
//                startActivity(intent );

            }
        });

        //跳轉Image畫面
        layout_image.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View view)
            {
                Intent intent = new Intent();
                intent.setClass(homepage.this,Image.class);
                startActivity(intent);

            }
        });

        layout_connect.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View view)
            {
                gotoUrl("https://csie.stust.edu.tw/");
            }
        });

    }
    private void gotoUrl(String s)
    {
        Uri uri = Uri.parse(s);
        startActivity(new Intent(Intent.ACTION_VIEW,uri));
    }
}