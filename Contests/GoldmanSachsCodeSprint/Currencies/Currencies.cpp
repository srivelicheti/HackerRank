// Currencies.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <map>
#include <vector>
#include <algorithm>
#include <cmath>
#include <fstream>

using namespace std;
typedef int ll;
const int mod = pow(10, 9) + 7;
long long SolveOne(ll*** mem, ll** cur ,int s ,int e, int k, int n)
{
	if (k <= 0)
		return -1;
	if (mem[k][s][e] != -1)
		return mem[k][s][e];
		
			ll cMax = -1;
		

			for (int z = 0; z<n; z++)
			{
				if (z == s || z == e)
					continue;
				ll tillZWithOneLess = SolveOne(mem,cur ,s, z, k - 1, n);
				if (tillZWithOneLess != -1) {
					ll tillZ = (tillZWithOneLess * cur[z][e]) % mod;
					cMax = max(cMax, tillZ);
				}
			}
			mem[k][s][e] = cMax;
		
	
			return mem[k][s][e];
}

int main()
{
#if _DEBUG
	std::ifstream ins("input.txt");
	std::streambuf *cinbuf = std::cin.rdbuf(); //save old buf
	std::cin.rdbuf(ins.rdbuf()); //redirect std::cin to in.txt!
#endif

	int n;
	cin >> n;

	int x, s, f, m;
	//int mod = pow(10, 9) + 7;

	cin >> x >> s >> f >> m;

	auto cur = new ll*[n];
	for(int i = 0;i<n;i++)
	{
		cur[i] = new ll[n];
		for(int j=0;j<n;j++)
		{
			cin >> cur[i][j];
		}
	}

	auto mem = new ll**[m + 1];
	for(int k = 0;k<=m;k++)
	{
		mem[k] = new ll*[n];
		for (int i = 0; i < n; i++)
		{
			mem[k][i] = new ll[n];
			for (int j = 0; j<n; j++)
			{
				mem[k][i][j] = -1;
			}
		}
	}

	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j<n; j++)
		{
			mem[0][i][j] = 0; // for 0 transactions 
			mem[1][i][j] = (1 * cur[i][j]) % mod; // for just one transaction the value is direct conversion
		}
	}

	/*for(int k=2;k<=m;k++)
	{
		for (int i = 0; i < n; i++)
		{
			
			for (int j = 0; j < n; j++)
			{
				ll cMax = 0;
				if(i==j)
					continue;

				for(int z = 0;z<n;z++)
				{
					if(z == i || z == j)
						continue;

					ll tillZ = ((mem[k - 1][i][z])*cur[z][j]) % mod;
					cMax = max(cMax, tillZ);
				}
				mem[k][i][j] = cMax;
			}
		}
	}
	cout << mem[m][s][f] << endl;*/

	cout << SolveOne(mem, cur, s, f, m, n);
	for (int k = 0; k <= m; k++)
	{
		for (int i = 0; i < n; i++)
		{
			delete[] mem[k][i];
		}
		delete[] mem[k];
	}
	delete[] mem;
    return 0;
}

