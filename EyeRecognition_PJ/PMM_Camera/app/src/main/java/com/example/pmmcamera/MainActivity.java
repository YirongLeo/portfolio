package com.example.pmmcamera;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity
{

    private static int SPLASH_TIME_OUT = 3000;

    View first, second, third, fourth, fifth, sixth;
    TextView a;
    ImageView logo;

    //開頭動畫
    Animation TopAnimation, BottomAnimation, MiddleAnimation;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_main);

        TopAnimation = AnimationUtils.loadAnimation(this,R.anim.top_animation);
        BottomAnimation = AnimationUtils.loadAnimation(this,R.anim.bottom_animation);
        MiddleAnimation = AnimationUtils.loadAnimation(this,R.anim.middle_animation);

        //Hooks
//        first = findViewById(R.id.first_line);
//        second = findViewById(R.id.second_line);
//        third = findViewById(R.id.third_line);
//        fourth = findViewById(R.id.fourth_line);
//        fifth = findViewById(R.id.fifth_line);
//        sixth = findViewById(R.id.sixth_line);

        a = findViewById(R.id.a);
        logo = findViewById(R.id.chi_mei_logo);

        //設定動畫
//        first.setAnimation(TopAnimation);
//        second.setAnimation(TopAnimation);
//        third.setAnimation(TopAnimation);
//        fourth.setAnimation(TopAnimation);
//        fifth.setAnimation(TopAnimation);
//        sixth.setAnimation(TopAnimation);

        a.setAnimation(MiddleAnimation);
        logo.setAnimation(MiddleAnimation);

        //跳轉畫面
        new Handler().postDelayed(new Runnable()
        {
            @Override
            public void run()
            {
                Intent intent = new Intent(MainActivity.this,homepage.class);
                startActivity(intent);
                finish();
            }
        },SPLASH_TIME_OUT);
    }
}