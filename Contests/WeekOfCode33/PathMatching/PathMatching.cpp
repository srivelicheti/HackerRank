#include "stdafx.h"
#include <fstream>
#include <iostream>
#include <vector>
#include <unordered_set>
#include <unordered_map>
#include <queue>
#include <algorithm>
#include <string>

using namespace std;

int countOccurances(const vector<vector<int>>& graph, const string& s, const string& p, int u, int v, vector<bool>& visited, 
	int& indexToMatch, int& count, vector<int>&  lps , int level , int& maxLevel )
{
	if (level > maxLevel)
		return -1;
	if (u == v)
	{
		return v;
	}
	int res = -1;
	for (auto c : graph[u])
	{
		if (!visited[c])
		{
			visited[c] = true;
			res = countOccurances(graph, s, p, c, v, visited, indexToMatch, count, lps, level+1,maxLevel);
			if (res >= 0)
			{
				if (p[indexToMatch] == s[c])
				{
					indexToMatch++;
					if (indexToMatch == p.length())
					{
						count++; indexToMatch = lps[indexToMatch - 1];
					}
				}
				else
				{
					indexToMatch = indexToMatch == 0 ? 0 : lps[indexToMatch - 1];
					while (indexToMatch >= 0) {
						if (p[indexToMatch] == s[c])
						{
							indexToMatch++;
							if (indexToMatch == p.length())
							{
								count++; indexToMatch = lps[indexToMatch - 1];
							}
							break;
						}
						if (indexToMatch == 0)
							break;
						indexToMatch = lps[indexToMatch - 1];
					}
				}
				break;
			}
		}
	}

	return res;
}

void countOccurancesBfs(const vector<vector<int>>& graph, const string& s, const string& p, int u, int v, vector<bool>& visited, int& indexToMatch, int& count, vector<int>&  lps)
{
	
	queue<int> q;
	queue<int> q2;
	q.push(u);
	q2.push(v);
	unordered_map<int, int> map;
	unordered_map<int, int> map2;
	map.insert({u,-1});
	//map.insert({ v,-1 });
	int middleIndex = -1;
	while(!q.empty() && !q2.empty() && middleIndex == -1)
	{
		auto cur = q.front();
		q.pop();
		if(map2.find(cur) != map2.end())
		{
			middleIndex = cur;
			break;
		}
		for (auto child : graph[cur])
		{
			if(!visited[child])
			{
				visited[child] = true;
				map.insert({ child, cur });
				

				q.push(child);
			}
			if (map2.find(child) != map2.end())
			{
				map.insert({ child, cur });
				middleIndex = child;
				break;
			}
		}
		if(middleIndex != -1)
			break;
		auto cur2 = q2.front();
		q2.pop();
		if(map.find(cur2) != map.end())
		{
			middleIndex = cur2;
			break;
		}
		for (auto child : graph[cur2])
		{
			if (!visited[child])
			{
				visited[child] = true;
				map2.insert({ cur2,child });
				q2.push(child);
			}
			if (map.find(child) != map.end())
			{
				map2.insert({ cur2,child });
				middleIndex = child;
				break;
			}
		}
	}

	auto t = v;
	auto map3 = map2;
	bool switched = false;
	while(t!=-1)
	{
		if (p[indexToMatch] == s[t])
		{
			indexToMatch++;
			if (indexToMatch == p.length())
			{
				count++; indexToMatch = lps[indexToMatch - 1];
			}
		}
		else
		{
			indexToMatch = indexToMatch == 0 ? 0 : lps[indexToMatch - 1];
			while (indexToMatch >= 0) {
				if (p[indexToMatch] == s[t])
				{
					indexToMatch++;
					if (indexToMatch == p.length())
					{
						count++; indexToMatch = lps[indexToMatch - 1];
					}
					break;
				}
				if (indexToMatch == 0)
					break;
				indexToMatch = lps[indexToMatch - 1];
			}
		}
		if(!switched && t == middleIndex)
		{
			map3 = map;
		}
		t = map3.find(t)->second;
	}

	return;
}

void Solve(const vector<vector<int>>& graph, const string& s, const string& p, int n, vector<int>&  lps)
{
	int v, u;
	cin >> v >> u;
	vector<bool> visisted(n + 1);

	int count = 0;
	visisted[u] = true;
	int indexToMatch = 0;
	bool found = false;
	int maxLevel = 50;
	while (!found) {
		maxLevel = maxLevel * 5;
		found = countOccurances(graph, s, p, u, v, visisted, indexToMatch, count, lps, 0, maxLevel);
	}
	if ( p[indexToMatch] == s[u])
	{
		indexToMatch++;
		if (indexToMatch == p.length())
		{
			count++; indexToMatch = lps[indexToMatch - 1];
		}

	}
	cout << count << endl;

}
// Fills lps[] for given patttern pat[0..M-1]
void computeLPSArray(string pat, vector<int>&  lps, int l)
{
	// length of the previous longest prefix suffix
	int len = 0;

	lps[0] = 0; // lps[0] is always 0

				// the loop calculates lps[i] for i = 1 to M-1
	int i = 1;
	while (i < l)
	{
		if (pat[i] == pat[len])
		{
			len++;
			lps[i] = len;
			i++;
		}
		else // (pat[i] != pat[len])
		{
			// This is tricky. Consider the example.
			// AAACAAAA and i = 7. The idea is similar 
			// to search step.
			if (len != 0)
			{
				len = lps[len - 1];

				// Also, note that we do not increment
				// i here
			}
			else // if (len == 0)
			{
				lps[i] = 0;
				i++;
			}
		}
	}
}

void computeTemporaryArray(string pattern, vector<int>& lps, int l) {

	int index = 0;
	lps[0] = 0;
	for (int i = 1; i < l;) {
		if (pattern[i] == pattern[index]) {
			lps[i] = index + 1;
			index++;
			i++;
		}
		else {
			if (index != 0) {
				index = lps[index - 1];
			}
			else {
				lps[i] = 0;
				i++;
			}
		}
	}
	return;
}

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(NULL);

#if _DEBUG
	std::ifstream ins("input.txt");
	std::streambuf *cinbuf = std::cin.rdbuf(); //save old buf
	std::cin.rdbuf(ins.rdbuf()); //redirect std::cin to in.txt!
#endif

	int n, q;
	cin >> n >> q;

	string s, p;
	cin >> s >> p;


	auto t = n - 1;
	s = ' ' + s;
	vector<vector<int>> graph(n + 1);

	while (t--)
	{
		int st, e;
		cin >> st >> e;
		graph[st].push_back(e);
		graph[e].push_back(st);
	}


	vector<int> lps(p.length());

	// Preprocess the pattern (calculate lps[] array)
	computeLPSArray(p, lps, p.length());

	while (q--)
	{
			Solve(graph, s, p, n, lps);
	}

	return 0;
}
