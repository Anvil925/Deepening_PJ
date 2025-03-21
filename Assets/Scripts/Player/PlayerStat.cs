using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float m_maxHp; // 최대 체력
    // public float m_maxShild; // 최대 쉴드
    // public float m_maxHungerPoint; // 최대 허기
    // public float m_maxStemina; // 최대 스테미나
    public float m_hp; // 현재 체력
    // public float m_shild; // 현재 쉴드
    // public float m_hungerPoint; // 현재 허기
    // public float m_stemina; // 현재 스테미나
    public float m_def; // 방어력
    public float m_atk; // 공격력
    public float m_atkSpeed; // 공격 속도
    public float m_critical; // 크리티컬 확률


    public PlayerStat(float hp, float shild, float hungerPoin, float stemina, float def, float atk, float atkSpeed, float critical)
    {
        m_maxHp = hp; // 최대 체력
        // m_maxShild = shild; // 최대 쉴드
        // m_maxHungerPoint = hungerPoin; // 최대 허기
        // m_maxStemina = stemina; // 최대 스테미나
        m_hp = hp; // 현재 체력
        // m_shild = shild; // 현재 쉴드
        // m_hungerPoint = hungerPoin; // 현재 허기
        // m_stemina = stemina; // 현재 스테미나
        m_def = def; // 방어력
        m_atk = atk; // 공격력
        m_atkSpeed = atkSpeed; // 공격 속도
        m_critical = critical; // 크리티컬 확률
    }

    public PlayerStat() // 기본 생성자
    {
        m_maxHp = 100; 
        // m_maxShild = 100;
        // m_maxHungerPoint = 100;
        // m_maxStemina = 100;
        m_hp = 100;
        // m_shild = 100;
        // m_hungerPoint = 100;
        // m_stemina = 100;
        m_def = 100;
        m_atk = 100;
        m_atkSpeed = 100;
        m_critical = 100;
    }

    public void SetMaxHp(float amount)
    {
        if (amount <= 0) return;

        m_maxHp = amount;
    }

    // public void SetMaxShild(float amount)
    // {
    //     if (amount <= 0) return;

    //     m_maxShild = amount;
    // }

    // public void SetMaxHungerPoint(float amount)
    // {
    //     if (amount <= 0) return;

    //     m_maxHungerPoint = amount;
    // }

    // public void SetMaxStemina(float amount)
    // {
    //     if (amount <= 0) return;

    //     m_maxStemina = amount;
    // }

    public void SetDef(float amount)
    {
        if (amount <= 0) return;

        m_def = amount;
    }

    public void SetAtk(float amount)
    {
        if (amount <= 0) return;

        m_atk = amount;
    }

    public void SetAtkSpeed(float amount)
    {
        if (amount <= 0) return;

        m_atkSpeed = amount;
    }

    public void SetCritical(float amount)
    {
        if (amount <= 0) return;

        m_critical = amount;
    }

    public void AddHp(float amount)
    {
        if (amount <= 0) return;

        m_hp += amount;

        if (m_hp > m_maxHp) m_hp = m_maxHp;
    }

    // public void AddShild(float amount)
    // {
    //     if (amount <= 0) return;

    //     m_shild += amount;

    //     if (m_shild > m_maxShild) m_shild = m_maxShild;
    // }

    // public void AddHungerPoint(float amount)
    // {
    //     if (amount <= 0) return;

    //     m_hungerPoint += amount;

    //     if (m_hungerPoint > m_maxHungerPoint) m_hungerPoint = m_maxHungerPoint;
    // }

    // public void AddStemina(float amount)
    // {
    //     if (amount <= 0) return;

    //     m_stemina += amount;

    //     if (m_stemina > m_maxStemina) m_stemina = m_maxStemina;
    // }

    public void SubHp(float amount)
    {
        if (amount <= 0 || m_hp == 0) return;

        m_hp -= amount;

        if (m_hp < 0) m_hp = 0;
    }

    // public float SubShild(float amount)
    // {
    //     if (amount <= 0) return 0;
    //     else if (m_shild == 0) return amount;

    //     m_shild -= amount;

    //     if (m_shild < 0)
    //     {
    //         amount = Mathf.Abs(m_shild);
    //         m_shild = 0;
    //         return amount;
    //     }

    //     return 0;
    // }

    // public void SubHungerPoint(float amount)
    // {
    //     if (amount <= 0 || m_hungerPoint == 0) return;

    //     m_hungerPoint -= amount;

    //     if (m_hungerPoint < 0) m_hungerPoint = 0;
    // }

    // public void SubStemina(float amount)
    // {
    //     if (amount <= 0 || m_stemina == 0) return;

    //     m_stemina -= amount;

    //     if (m_stemina < 0) m_stemina = 0;
    // }
}