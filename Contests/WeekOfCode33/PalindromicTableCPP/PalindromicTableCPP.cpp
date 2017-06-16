// PalindromicTableCPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string>
#include <fstream>
#include <iostream>
using namespace std;

class Numbers
{
private:
	int privateZero = 0;
	int privateOne = 0;
	int privateTwo = 0;
	int privateThree = 0;
	int privateFour = 0;
	int privateFive = 0; int privateSix = 0;
	int privateSeven = 0;
	int privateEight = 0;
	int privateNine = 0;

public:
	 Numbers();

	 Numbers(int n);

	 Numbers(int z, int o, int t, int thr, int f, int fiv, int s, int seve, int ei, int ni);

	int  getZero() const;

	void  setZero(const int &value);

	int  getOne() const;

	void  setOne(const int &value);

	int  getTwo() const;

	void  setTwo(const int &value);

	int  getThree() const;

	void  setThree(const int &value);

	int  getFour() const;

	void  setFour(const int &value);
	int  getFive() const;

	void  setFive(const int &value);

	int  getSix() const;

	void  setSix(const int &value);

	int  getSeven() const;

	void  setSeven(const int &value);

	int  getEight() const;

	void  setEight(const int &value);

	int  getNine() const;

	void  setNine(const int &value);

	Numbers*  Add(int n);

	Numbers*  AddZero();

	Numbers*  AddONe();

	Numbers*  AddTwo();

	Numbers*  AddThree();

	Numbers*  AddFour();

	Numbers*  AddFive();

	Numbers*  AddSix();
	Numbers*  AddSeven();
	Numbers*  AddEight();

	Numbers*  AddNine();

	Numbers*  operator + (const Numbers &n2);

	Numbers*  operator - (const Numbers &n2);

	int  PalinMaxLen();

};


	Numbers::Numbers()
	{
	}

	Numbers::Numbers(int n)
	{
		if (n == 0)
		{
			setZero(1);
		}

		if (n == 1)
		{
			setOne(1);
		}

		if (n == 2)
		{
			setTwo(1);
		}


		if (n == 3)
		{
			setThree(1);
		}


		if (n == 4)
		{
			setFour(1);
		}


		if (n == 5)
		{
			setFive(1);
		}


		if (n == 6)
		{
			setSix(1);
		}


		if (n == 7)
		{
			setSeven(1);
		}


		if (n == 8)
		{
			setEight(1);
		}


		if (n == 9)
		{
			setNine(1);
		}
	}

	Numbers::Numbers(int z, int o, int t, int thr, int f, int fiv, int s, int seve, int ei, int ni)
	{
		setZero(z);
		setOne(o);
		setTwo(t);
		setThree(thr);
		setFour(f);
		setFive(fiv);
		setSix(s);
		setSeven(seve);
		setEight(ei);
		setNine(ni);
	}

	int Numbers::getZero() const
	{
		return privateZero;
	}

	void Numbers::setZero(const int &value)
	{
		privateZero = value;
	}

	int Numbers::getOne() const
	{
		return privateOne;
	}

	void Numbers::setOne(const int &value)
	{
		privateOne = value;
	}

	int Numbers::getTwo() const
	{
		return privateTwo;
	}

	void Numbers::setTwo(const int &value)
	{
		privateTwo = value;
	}

	int Numbers::getThree() const
	{
		return privateThree;
	}

	void Numbers::setThree(const int &value)
	{
		privateThree = value;
	}

	int Numbers::getFour() const
	{
		return privateFour;
	}

	void Numbers::setFour(const int &value)
	{
		privateFour = value;
	}

	int Numbers::getFive() const
	{
		return privateFive;
	}

	void Numbers::setFive(const int &value)
	{
		privateFive = value;
	}

	int Numbers::getSix() const
	{
		return privateSix;
	}

	void Numbers::setSix(const int &value)
	{
		privateSix = value;
	}

	int Numbers::getSeven() const
	{
		return privateSeven;
	}

	void Numbers::setSeven(const int &value)
	{
		privateSeven = value;
	}

	int Numbers::getEight() const
	{
		return privateEight;
	}

	void Numbers::setEight(const int &value)
	{
		privateEight = value;
	}

	int Numbers::getNine() const
	{
		return privateNine;
	}

	void Numbers::setNine(const int &value)
	{
		privateNine = value;
	}

	Numbers* Numbers::Add(int n)
	{
		if (n == 0)
		{
			return AddZero();
		}

		else if (n == 1)
		{
			return AddONe();
		}

		else if (n == 2)
		{
			return AddTwo();
		}


		else if (n == 3)
		{
			return AddThree();
		}


		else if (n == 4)
		{
			return AddFour();
		}


		else if (n == 5)
		{
			return AddFive();
		}


		else if (n == 6)
		{
			return AddSix();
		}


		else if (n == 7)
		{
			return AddSeven();
		}


		else if (n == 8)
		{
			return AddEight();
		}


		else if (n == 9)
		{
			return AddNine();
		}
		else
		{
			//C# TO C++ CONVERTER TODO TASK: The std::exception constructor has no parameters:
			//ORIGINAL LINE: throw new Exception("INvalid number");
			throw std::exception();
		}
	}

	Numbers* Numbers::AddZero()
	{
		auto temp = new Numbers(getZero() + 1, getOne(), getTwo(), getThree(), getFour(), getFive(), getSix(), getSeven(), getEight(), getNine());
		return temp;
	}

	Numbers* Numbers::AddONe()
	{
		return new Numbers(getZero(), getOne() + 1, getTwo(), getThree(), getFour(), getFive(), getSix(), getSeven(), getEight(), getNine());
	}

	Numbers* Numbers::AddTwo()
	{
		return new  Numbers(getZero(), getOne(), getTwo() + 1, getThree(), getFour(), getFive(), getSix(), getSeven(), getEight(), getNine());
	}

	Numbers* Numbers::AddThree()
	{
		return  new Numbers(getZero(), getOne(), getTwo(), getThree() + 1, getFour(), getFive(), getSix(), getSeven(), getEight(), getNine());
	}

	Numbers* Numbers::AddFour()
	{
		return  new  Numbers(getZero(), getOne(), getTwo(), getThree(), getFour() + 1, getFive(), getSix(), getSeven(), getEight(), getNine());
	}

	Numbers* Numbers::AddFive()
	{
		return  new  Numbers(getZero(), getOne(), getTwo(), getThree(), getFour(), getFive() + 1, getSix(), getSeven(), getEight(), getNine());
	}

	Numbers* Numbers::AddSix()
	{
		return  new  Numbers(getZero(), getOne(), getTwo(), getThree(), getFour(), getFive(), getSix() + 1, getSeven(), getEight(), getNine());
	}

	Numbers* Numbers::AddSeven()
	{
		return  new  Numbers(getZero(), getOne(), getTwo(), getThree(), getFour(), getFive(), getSix(), getSeven() + 1, getEight(), getNine());
	}

	Numbers* Numbers::AddEight()
	{
		return  new Numbers(getZero(), getOne(), getTwo(), getThree(), getFour(), getFive(), getSix(), getSeven(), getEight() + 1, getNine());
	}

	Numbers* Numbers::AddNine()
	{
		return  new  Numbers(getZero(), getOne(), getTwo(), getThree(), getFour(), getFive(), getSix(), getSeven(), getEight(), getNine() + 1);
	}

	Numbers* Numbers::operator + (const Numbers &n2)
	{
		return  new Numbers(this->getZero() + n2.getZero(), this->getOne() + n2.getOne(), this->getTwo() + n2.getTwo(), this->getThree() + n2.getThree(), this->getFour() + n2.getFour(), this->getFive() + n2.getFive(), this->getSix() + n2.getSix(), this->getSeven() + n2.getSeven(), this->getEight() + n2.getEight(), this->getNine() + n2.getNine());
	}

	Numbers* Numbers::operator - (const Numbers &n2)
	{
		return  new Numbers(this->getZero() - n2.getZero(), std::abs(this->getOne() - n2.getOne()), std::abs(this->getTwo() - n2.getTwo()), std::abs(this->getThree() - n2.getThree()), std::abs(this->getFour() - n2.getFour()), std::abs(this->getFive() - n2.getFive()), std::abs(this->getSix() - n2.getSix()), std::abs(this->getSeven() - n2.getSeven()), std::abs(this->getEight() - n2.getEight()), std::abs(this->getNine() - n2.getNine()));
	}

	int Numbers::PalinMaxLen()
	{
		int palinLen = 0;
		bool oddFound = false;

		if (getOne() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getOne();

		if (getTwo() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getTwo();

		if (getThree() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getThree();

		if (getFour() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getFour();

		if (getFive() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getFive();

		if (getSix() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getSix();

		if (getSeven() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getSeven();

		if (getEight() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getEight();

		if (getNine() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getNine();

		if (getZero() % 2 == 1)
		{
			if (oddFound)
			{
				return -1;
			}
			oddFound = true;
		}
		palinLen += getZero();

		if (palinLen == getZero() || palinLen == (getZero() - 1))
		{
			return -1;
		}
		return palinLen;

	}

	

Numbers* AddSub(Numbers* n1 ,Numbers* n2, Numbers* n3)
{
	auto z = n1->getZero() + n2->getZero() - n3->getZero();
	auto o = n1->getOne() + n2->getOne() - n3->getOne();
	auto t= n1->getTwo() + n2 ->getTwo() - n3->getTwo();
	auto th= n1->getThree() + n2 ->getThree() - n3->getThree();
	auto f= n1->getFour() + n2 ->getFour() - n3->getFour();
	auto fi= n1->getFive() + n2 ->getFive() - n3->getFive();
	auto s= n1->getSix() + n2 ->getSix() - n3->getSix();
	auto se= n1->getSeven() + n2 ->getSeven() - n3->getSeven();
	auto e= n1->getEight() + n2 ->getEight() - n3->getEight();
	auto nin= n1->getNine() + n2 ->getNine() - n3->getNine();
	
	return new Numbers(z, o, t, th, f, fi, s, se, e, nin);

}

Numbers* Sub(Numbers* n2, Numbers* n3)
{
	auto z = n2->getZero() - n3->getZero();
	auto o =  n2->getOne() - n3->getOne();
	auto t =  n2->getTwo() - n3->getTwo();
	auto th = n2->getThree() - n3->getThree();
	auto f =  n2->getFour() - n3->getFour();
	auto fi = n2->getFive() - n3->getFive();
	auto s =  n2->getSix() - n3->getSix();
	auto se =  n2->getSeven() - n3->getSeven();
	auto e =  n2->getEight() - n3->getEight();
	auto nin = n2->getNine() - n3->getNine();

	return new Numbers(z, o, t, th, f, fi, s, se, e, nin);

}



int CalcMaxLen(Numbers ***numCache, int si, int sj, int ei, int ej)
{
	if (si == ei && sj == ej)
	{
		return 1;
	}

	auto total = numCache[ei][ej];

	int toDelj = sj - 1;
	if (toDelj >= 0)
	{
		total =	Sub(total , numCache[ei][toDelj]);
	}

	int toDeli = si - 1;
	if (toDeli >= 0)
	{
		total = Sub((total) , (numCache[toDeli][ ej]));
	}

	return total->PalinMaxLen();
}

int calcArea(int si, int sj, int ei, int ej)
{
	return (ei - si + 1) * (ej - sj + 1);
}

int main()
{

#if _DEBUG
	std::ifstream ins("input.txt");
	std::streambuf *cinbuf = std::cin.rdbuf(); //save old buf
	std::cin.rdbuf(ins.rdbuf()); //redirect std::cin to in.txt!
#endif

	ios::sync_with_stdio(false);
	cin.tie(NULL);

	int m, n;
	cin >> m >> n;

	auto inp = new int*[m];
	auto numCache = new Numbers**[m];
	for (int i = 0; i < m; i++)
	{
		inp[i] = new int[n];
		numCache[i] = new Numbers*[n];
		for (int j = 0; j < n; j++)
		{
			cin >> inp[i][j];
			numCache[i][ j] = new Numbers();
		}
	}
	
	
	numCache[0][0] = new Numbers(inp[0][0]);
	
	for (int j = 1; j < n; j++)
	{
		numCache[0][j] = numCache[0][j - 1]->Add(inp[0][ j]);
	}

	for (int i = 1; i < m; i++)
	{
		auto z = i - 1;
		auto temp = numCache[z][0]->Add(inp[i][ 0]);
		numCache[i][ 0] = temp;
	}

	for (int i = 1; i < m; i++)
	{
		for (int j = 1; j < n; j++)
		{
			numCache[i][ j] = AddSub(numCache[i][ j - 1], numCache[i - 1][ j], numCache[i - 1][ j - 1])->Add(inp[i][ j]);
			
		}
	}
	int maxLen = 0;
	int msi = -1, msj = -1, mei = -1, mej = -1;

	for (int ei = m -1; ei >= 0; ei--)
	{
		for (int ej = n -1; ej >= 0; ej--)
		{
			if (calcArea(0, 0, ei, ej) < maxLen)
				continue;
			for (int si = 0; si <= ei; si++)
			{
				for (int sj = 0; sj <= ej; sj++)
				{
					if (calcArea(si, sj, ei, ej) < maxLen)
						continue;
					int cMaxLen = CalcMaxLen(numCache, si, sj, ei, ej);
					if (cMaxLen > maxLen)
					{
						maxLen = cMaxLen;
						msi = si;
						msj = sj;
						mei = ei;
						mej = ej;
					}
				}
			}
		}
	}
	cout << maxLen << endl;
	cout << msi << " " << msj << " " << mei << " " << mej << endl;

    return 0;
}

