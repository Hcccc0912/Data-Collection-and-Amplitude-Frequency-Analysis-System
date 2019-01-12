library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity FrequenCnt is
	port(	clk		:in std_logic;
			reset		:in std_logic;
			fx			:in std_logic;
			complete	:out std_logic;
			Nx			:out integer range 50000000 downto 0;
			Ns			:out integer range 50000000 downto 0);
end FrequenCnt;

architecture behav of FrequenCnt is

signal tNx,tNs:integer range 50000000 downto 0;
signal tnotQ,ttp,tQ:std_logic;

component GateSigGen is
	port(	clk	:in std_logic;
			reset	:in std_logic;
			tp		:out std_logic);
end component;

component DTrig is
	port(	cp		:in std_logic;
			D		:in std_logic;
			Q		:out std_logic;
			notQ	:out std_logic);
end component;

component FreCnt is
	port(	en		:in std_logic;
			clk	:in std_logic;
			reset	:in std_logic;
			Nx		:out integer range 50000000 downto 0);
end component;

begin
	u0:GateSigGen
		port map(clk,reset,ttp);
	u1:DTrig
		port map(fx,ttp,tQ,tnotQ);
	u2:FreCnt
		port map(tQ,fx,reset,tNx);
	u3:FreCnt
		port map(tQ,clk,reset,tNs);
	
	complete <= not tQ;
	Nx <= tNx;
	Ns <= tNs;
	
end behav;