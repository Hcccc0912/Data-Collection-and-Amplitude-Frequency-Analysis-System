library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity SmgTopLevel is
	port(	clk		:in std_logic;
			rst_n		:in std_logic;
			SmgData	:in std_logic_vector(23 downto 0);
			SmgDuan	:out std_logic_vector(7 downto 0);
			SmgWei	:out std_logic_vector(5 downto 0));
end SmgTopLevel;

architecture behav of SmgTopLevel is

component SmgDataSeg is
	port(	clk		:in std_logic;
			rst_n		:in std_logic;
			Data		:in std_logic_vector(23 downto 0);
			DataSeg	:out std_logic_vector(3 downto 0));
end component;

component SmgDuanCode is
	generic(
				dis0:std_logic_vector(7 downto 0):=b"1100_0000";
				dis1:std_logic_vector(7 downto 0):=b"1111_1001";
				dis2:std_logic_vector(7 downto 0):=b"1010_0100";
				dis3:std_logic_vector(7 downto 0):=b"1011_0000";
				dis4:std_logic_vector(7 downto 0):=b"1001_1001";
				dis5:std_logic_vector(7 downto 0):=b"1001_0010";
				dis6:std_logic_vector(7 downto 0):=b"1000_0010";
				dis7:std_logic_vector(7 downto 0):=b"1111_1000";
				dis8:std_logic_vector(7 downto 0):=b"1000_0000";
				dis9:std_logic_vector(7 downto 0):=b"1001_0000";

				rec0:std_logic_vector(3 downto 0):=b"0000";
				rec1:std_logic_vector(3 downto 0):=b"0001";
				rec2:std_logic_vector(3 downto 0):=b"0010";
				rec3:std_logic_vector(3 downto 0):=b"0011";
				rec4:std_logic_vector(3 downto 0):=b"0100";
				rec5:std_logic_vector(3 downto 0):=b"0101";
				rec6:std_logic_vector(3 downto 0):=b"0110";
				rec7:std_logic_vector(3 downto 0):=b"0111";
				rec8:std_logic_vector(3 downto 0):=b"1000";
				rec9:std_logic_vector(3 downto 0):=b"1001");
	port(		clk		:in std_logic;
				rst_n		:in std_logic;
				Data		:in std_logic_vector(3 downto 0);
				State		:in std_logic_vector(5 downto 0);
				DuanCode	:out std_logic_vector(7 downto 0));
end component;

component SmgWeiCode is
	port(	clk	:in std_logic;
			rst_n	:in std_logic;
			Wei	:out std_logic_vector(5 downto 0));
end component;

signal DataSegTemp:std_logic_vector(3 downto 0);
signal WeiTemp:std_logic_vector(5 downto 0);

begin
	u1:SmgDataSeg
		port map(clk,rst_n,SmgData,DataSegTemp);
	u2:SmgDuanCode
		port map(clk,rst_n,DataSegTemp,WeiTemp,SmgDuan);
	u3:SmgWeiCode
		port map(clk,rst_n,WeiTemp);
SmgWei <= WeiTemp;
end behav;