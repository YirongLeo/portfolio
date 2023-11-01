package com.example.topicproject;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class login extends AppCompatActivity {

    EditText e1,e2;
    Button b1,b2;
    DataBase db;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        db = new DataBase(this);
        e1 = (EditText)findViewById(R.id.loginemail);
        e2 = (EditText)findViewById(R.id.loginpass);
        b1 = (Button)findViewById(R.id.button);
        b2 = (Button)findViewById(R.id.button1);

        //進入到主頁面
        b1.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                String email = e1.getText().toString();
                String password = e2.getText().toString();
                Boolean chkemailpass = db.emailpassword(email,password);
                if (chkemailpass == true) {
                    Toast.makeText(getApplicationContext(), "登入成功", Toast.LENGTH_SHORT).show();
                    Intent i = new Intent(login.this,FuctionPage.class);
                    startActivity(i);
                } else {
                    Toast.makeText(getApplicationContext(), "登入失敗", Toast.LENGTH_SHORT).show();
                }
            }
        });

        //進入到註冊頁面
        b2.setOnClickListener(v -> {

            Intent intent = new Intent(this,register.class);
            startActivity(intent);
        });
    }
}