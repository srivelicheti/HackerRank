// BonnieAndClydeCPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
// BonnieAndClydeCPP.cpp : Defines the entry point for the console application.
//

#include <fstream>
#include <iostream>
#include <vector>
#include <unordered_set>
#include <unordered_map>
#include <queue>
#include <algorithm>


using namespace std;

int FindPathDfs(int s, int d, vector<vector<int>>& graph, unordered_set<int>* path, vector<bool>& visited)
{
	if (s == d) {
		path->insert(s);
		return s;
	}

	
		for (auto i : graph[s])
		{
			if (!visited[i])
			{
				visited[i] = true;
				auto found = FindPathDfs(i, d, graph, path, visited);
				if (found >= 0)
				{
					path->insert(s);
					return s;
				}
			}
		}
	return -1;
}

unordered_set<int>* FindPath2(int s, int d, vector<vector<int>>& graph, int n, unordered_map<string, vector<unordered_set<int>>>& pathsCache, int u ,int v ,int w)
{
	unordered_set<int>* path = nullptr;
	/*auto cachedPaths = pathsCache.find(s + "_" + d);
	if(cachedPaths != pathsCache.end())
	{
		path = cachedPaths->second;
	}
	if (path != nullptr)
		return path;*/
	path = new unordered_set<int>();
	vector<bool> visited(n + 1);
	visited[s] = true;
	visited[w] = true;
	if (d == v)
		visited[u] = true;
	else
		visited[v] = true;
	auto found = FindPathDfs(s, d, graph, path, visited);

	if (found >= 0) {
	//	pathsCache.insert({ s + "_" + d, path });
		return path;
	}
	return nullptr;
}

int FindOtherPathDfs(int s, int d,  int startPoint ,vector<vector<int>>& graph, unordered_set<int>* path, vector<bool>& visited, unordered_set<int>* existingPath)
{
	if (existingPath->find(s) != existingPath->end() && s != startPoint && s!=d)
		return -1;

	if (s == d) {
		path->insert(s);
		return s;
	}

		for (auto i : graph[s])
		{
			if (!visited[i])
			{
				visited[i] = true;
				if (existingPath->find(i) == existingPath->end() || i== startPoint || i == d) {
					auto found = FindOtherPathDfs(i, d, startPoint ,graph, path, visited, existingPath);
					if (found >= 0)
					{
						path->insert(s);
						return s;
					}
				}
			}
		}
	return -1;
}

bool AreDisoint(unordered_set<int> s1, unordered_set<int> s2)
{
	if (s1.empty() || s2.empty())
		return true;

	for (auto i : s1)
	{
		if (s2.find(i) != s2.end())
			return false;
	}
	return true;
}

bool AreDisoint(unordered_set<int>* s1, unordered_set<int>* s2)
{
	if (s1->empty() || s2->empty())
		return true;

	for (auto i : *s1)
	{
		if (s2->find(i) != s2->end())
			return false;
	}
	return true;
}

unordered_set<int>* FindOtherPath2(int s, int d, vector<vector<int>>& graph,  unordered_set<int>* existingPath, int n, unordered_map<string, vector<unordered_set<int>>>& pathsCache , int u, int v, int w)
{
	/*auto cachedPaths = pathsCache.equal_range(s + "_" + d);
	for (auto pc = cachedPaths.first; pc != cachedPaths.second;++pc)
	{
		if( AreDisoint(existingPath,pc->second))
		{
			return  pc->second;
		}
	}*/

	vector<bool> visited(n+1);
	visited[s] = true;
	visited[w] = true;
	if (d == v)
		visited[u] = true;
	else
		visited[v] = true;
	auto path = new unordered_set<int>();
	auto found = FindOtherPathDfs(s, d, s,graph, path, visited, existingPath);

	if (found >= 0) {
		//pathsCache.insert({ s + "_" + d, path });
		return path;
	}
	return nullptr;
}


void SolveOne(vector<vector<int>>& graph, int n)
{
	unordered_map<string, vector<unordered_set<int>>> pathsCache;// = new unordered_map<string, vector<unordered_set<int>>>();
	int u, v, w;
	cin >> u >> v >> w;

	unordered_map<int, vector<unordered_set<int>*>> uPaths;
	unordered_map<int, vector<unordered_set<int>*>> vPaths;

	if ((u != w && v != w) && (graph[w].size() == 1))
	{
		cout << "NO" << endl;
		return;
	}
	if (u == w && v == w) {
		cout << "YES" << endl;
		return;
	}

	vector<int> towPathsFoundU;
	vector<int> twoPathsFoundV;
	sort(graph[w].begin(), graph[w].end(), [&graph](const int a, const int b)
	{
		return graph[a].size() > graph[b].size();
	});

	{
		for (auto src : graph[w])
		{
			if (src != v && u != w)
			{
				unordered_set<int>* path = nullptr;
				//auto find = pathsCache.find(src + " " + u);
				//if (find == pathsCache.end())
					path = FindPath2(src, u, graph, n, pathsCache, u , v ,w);
				//else
					//path = &find->second[0];
				if (path)
				{
					vector<unordered_set<int>*> ins;
					ins.push_back(path);
					uPaths.insert({ src,ins });
					
					
					auto path2 = FindOtherPath2(src, u, graph,  path, n, pathsCache, u ,v ,w);
					if (path2) {
						towPathsFoundU.push_back(src);
						if (twoPathsFoundV.size()>1)
						{
							cout << "YES" << endl;
							return;
						}
						uPaths.find(src)->second.push_back(path2);
					}
				}

			}
			if (v != w && src != u)
			{
				auto path = FindPath2(src, v, graph,  n,pathsCache, u , v ,w);
				if (path)
				{
					vector<unordered_set<int>*> ins;
					ins.push_back(path);
					vPaths.insert({ src, ins });
					
					auto path2 = FindOtherPath2(src, v, graph,  path, n , pathsCache, u ,v ,w);
					if (path2) {
						twoPathsFoundV.push_back(src);
						if (towPathsFoundU.size() > 1)
						{
							cout << "YES" << endl;
							return;
						}
						vPaths.find(src)->second.push_back(path2);

					}
				}
			}

		}
	}


	if ((u == w && vPaths.size() > 0) || (v == w && uPaths.size()>0) || (u == w && v == w))
	{
		cout << "YES" << endl;
		return;
	}

	for (auto u_path : uPaths)
	{
		if (u_path.second.size() > 0)
		{
			for (auto p1 : u_path.second)
			{
				for (auto v_path : vPaths)
				{
					if (v_path.first != u_path.first && v_path.second.size() > 0)
					{
						for (auto path : v_path.second)
						{
							if (AreDisoint(path, p1))
							{
								cout << "YES" << endl;
								return;
							}
						}
					}
				}
			}

		}
	}
	cout << "NO" << endl;
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

	int n, m, q;
	cin >> n >> m >> q;

	vector<vector<int>> graph(n + 1);

	

	while (m--)
	{
		int s, e;
		cin >> s >> e;
		graph[s].push_back(e);
		graph[e].push_back(s);
	}

	

	while (q--) {

		SolveOne(graph, n);
	}
	/*for (auto it = pathsCache->begin(); it != pathsCache->end(); ++it)
		delete it->second;
	delete pathsCache;*/

	return 0;
}



