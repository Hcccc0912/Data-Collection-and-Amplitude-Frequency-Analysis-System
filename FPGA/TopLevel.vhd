library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity TopLevel is
	port(	clk		:in std_logic;
			rst_n		:in std_logic;
			fx			:in std_logic;
			SmgDuan	:out std_logic_vector(7 downto 0);
			SmgWei	:out std_logic_vector(5 downto 0));
end TopLevel;

architecture behav of TopLevel is

component FrequenCnt is
	port(	clk		:in std_logic;
			reset		:in std_logic;
			fx			:in std_logic;
			complete	:out std_logic;
			Nx			:out integer range 50000000 downto 0;
			Ns			:out integer range 50000000 downto 0);
end component;

component SmgTopLevel is
	port(	clk		:in std_logic;
			rst_n		:in std_logic;
			SmgData	:in std_logic_vector(23 downto 0);
			SmgDuan	:out std_logic_vector(7 downto 0);
			SmgWei	:out std_logic_vector(5 downto 0));
end component;

component DataTrans is
	port(	clk		:in std_logic;
			Nx			:in integer range 50000000 downto 0;
			Ns			:in integer range 50000000 downto 0;
			SmgData	:out std_logic_vector(23 downto 0));
end component;

signal NxTemp,NsTemp:integer range 50000000 downto 0;
signal SmgDataTemp:std_logic_vector(23 downto 0);
signal Temp:std_logic_vector(23 downto 0);
signal complete:std_logic;

begin	
	u1:FrequenCnt
		port map(clk,rst_n,fx,complete,NxTemp,NsTemp);
	u2:SmgTopLevel
		port map(clk,rst_n,SmgDataTemp,SmgDuan,SmgWei);
	u3:DataTrans
		port map(clk,NxTemp,NsTemp,Temp);

	process(complete)
	begin	
		if(rising_edge(complete))then
			SmgDataTemp <= Temp;
		end if;
	end process;
end behav;