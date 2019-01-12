library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity SmgDataSeg is
	port(clk: in std_logic;
		rst_n: in std_logic;
		Data: in std_logic_vector(23 downto 0);
		DataSeg: out std_logic_vector(3 downto 0));
end SmgDataSeg;

architecture behav of SmgDataSeg is
constant T1MS:integer:=4_9999;
signal count:integer range 5_0000 downto 0;
signal SelectWei:integer range 5 downto 0;
begin

process(clk,rst_n)
begin
	if(rst_n = '0')then
		count <= 0;
	elsif(rising_edge(clk))then
		if(count = T1MS)then
			count <= 0;
		else
			count <= count + 1;
		end if;
	end if;
end process;

process(clk,rst_n)
begin
	if(rst_n = '0')then
		SelectWei <= 0;
		DataSeg <= b"0000";
	elsif(rising_edge(clk))then
		case SelectWei is
			when 0 => if(count = T1MS)then SelectWei <= SelectWei + 1;else DataSeg <= Data(23 downto 20);end if;
			when 1 => if(count = T1MS)then SelectWei <= SelectWei + 1;else DataSeg <= Data(19 downto 16);end if;
			when 2 => if(count = T1MS)then SelectWei <= SelectWei + 1;else DataSeg <= Data(15 downto 12);end if;
			when 3 => if(count = T1MS)then SelectWei <= SelectWei + 1;else DataSeg <= Data(11 downto 8);end if;
			when 4 => if(count = T1MS)then SelectWei <= SelectWei + 1;else DataSeg <= Data(7 downto 4);end if;
			when 5 => if(count = T1MS)then SelectWei <= 0;else DataSeg <= Data(3 downto 0);end if;
		end case;
	end if;
end process;
end behav;