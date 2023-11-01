package com.example.pmmcamera;

import androidx.activity.result.ActivityResultCallback;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;

public class Image extends AppCompatActivity
{
    Button btn;
    ImageView imageView;


    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_image);

        btn = findViewById(R.id.selectbtn);
        imageView = findViewById(R.id.pickedimage);

        // run the blow method on imageview click
        btn.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                mGetContent.launch("image/*");
            }
        });

    }

    //start an activity for result (new method)
    ActivityResultLauncher<String> mGetContent = registerForActivityResult(new ActivityResultContracts.GetContent(),
            new ActivityResultCallback<Uri>()
            {
                @Override
                public void onActivityResult(Uri result)
                {
                    if (result != null)
                    {
                        imageView.setImageURI(result);
                    }
                }
            });

}