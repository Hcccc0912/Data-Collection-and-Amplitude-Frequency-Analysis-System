library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity SmgDuanCode is
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
	port(		clk:in std_logic;
				rst_n:in std_logic;
				Data:in std_logic_vector(3 downto 0);
				State:in std_logic_vector(5 downto 0);
				DuanCode:out std_logic_vector(7 downto 0));
end SmgDuanCode;

architecture behav of SmgDuanCode is
signal ReadData:std_logic_vector(3 downto 0);
begin
process(clk,rst_n)
begin
	if(rst_n = '0')then
		DuanCode <= b"0000_0000";
	elsif(rising_edge(clk))then
		ReadData <= Data;
		case State is
			when b"101_111"|b"111_011" =>		case ReadData is
															when rec0 => DuanCode <= dis0 ;
															when rec1 => DuanCode <= dis1 ;
															when rec2 => DuanCode <= dis2 ;
															when rec3 => DuanCode <= dis3 ;
															when rec4 => DuanCode <= dis4 ;
															when rec5 => DuanCode <= dis5 ;
															when rec6 => DuanCode <= dis6 ;
															when rec7 => DuanCode <= dis7 ;
															when rec8 => DuanCode <= dis8 ;
															when rec9 => DuanCode <= dis9 ;
															when others => DuanCode <= b"0000_0000";
														end case;
			when others => 						case ReadData is
															when rec0 => DuanCode <= dis0;
															when rec1 => DuanCode <= dis1;
															when rec2 => DuanCode <= dis2;
															when rec3 => DuanCode <= dis3;
															when rec4 => DuanCode <= dis4;
															when rec5 => DuanCode <= dis5;
															when rec6 => DuanCode <= dis6;
															when rec7 => DuanCode <= dis7;
															when rec8 => DuanCode <= dis8;
															when rec9 => DuanCode <= dis9;
															when others => DuanCode <= b"0000_0000";
														end case;
		end case;
	end if;
end process;
end behav;