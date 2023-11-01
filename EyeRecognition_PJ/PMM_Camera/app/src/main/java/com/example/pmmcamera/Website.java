package com.example.pmmcamera;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;


public class Website extends AppCompatActivity
{

    TextView inst;
    Button twitter;
    ImageView youtube;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_website);

        inst = findViewById(R.id.textView);
        twitter = findViewById(R.id.button);
        youtube = findViewById(R.id.imageView);

        inst.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                gotoUrl("http://www.chimei.org.tw/");
            }

        });

    }

    private void gotoUrl(String s)
    {
        Uri uri = Uri.parse(s);
        startActivity(new Intent(Intent.ACTION_VIEW,uri));
    }
}