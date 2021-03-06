library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;
--顶层文件
entity top is 
	port(clk:in std_logic;
		rst_n:in std_logic;
		fx:in std_logic;
		rx:in std_logic;
		addata:in std_logic_vector(7 downto 0);
		adclk:out std_logic;
		tx:out std_logic;
		SmgDuan:out std_logic_vector(7 downto 0);
		SmgWei:out std_logic_vector(5 downto 0));
end top;
architecture behavioral of top is
--pll模块 产生9.6M时钟 供给adclk与uart对应0.6Mbps
component pll IS
	PORT
	(
		areset		: IN STD_LOGIC  := '0';
		inclk0		: IN STD_LOGIC  := '0';
		c0				: OUT STD_LOGIC ;			--9.6M
		locked		: OUT STD_LOGIC 
	);
END component;
--uart发送模块
component uarttx is
	port(clk:in std_logic;
		rst_n:in std_logic;
		datain:in std_logic_vector(7 downto 0);
		wrsig:in std_logic;
		idle:out std_logic;
		tx: out std_logic);
end component;
--uart接收模块
component uartrx is
	port(clk:in std_logic;
		rst_n:in std_logic;
			rx:in std_logic;
		dataout:out std_logic_vector(7 downto 0);
		rdsig:out std_logic;
		frameerror:out std_logic);
end component;
--采样模块
component an108 is
	port(clk:in std_logic;
		adclk:out std_logic;
		datain:in std_logic_vector(7 downto 0);
		dataout:out std_logic_vector(7 downto 0));
end component;
--频率测量模块
component TopLevel is
	port(	clk		:in std_logic;
			rst_n		:in std_logic;
			fx			:in std_logic;
			SmgDuan	:out std_logic_vector(7 downto 0);
			SmgWei	:out std_logic_vector(5 downto 0));
end component;
--内部信号定义
signal uartclk:std_logic;
signal addata_inter:std_logic_vector(7 downto 0);
signal rxdata_inter:std_logic_vector(7 downto 0);
signal rxwrsig,txwrsig:std_logic;
signal txdata_inter:std_logic_vector(7 downto 0);
--状态定义
type state_type is (s0,s1);
type send_type is (t0,t1);
signal current_state:state_type;
signal next_state:state_type;
signal send_state:send_type;
--发送状态所需信号
signal uart_cnt:integer;
signal send_count:integer;
signal send_temp:std_logic_vector(7 downto 0);--待发送的缓冲
signal count:integer;
constant T10MS:integer:= 9_600_000;
begin
u1:pll port map('0',clk,uartclk,open);
u2:an108 port map(uartclk,adclk,addata,addata_inter);
u3:uartrx port map(uartclk,rst_n,rx,rxdata_inter,rxwrsig,open);
u4:uarttx port map(uartclk,rst_n,txdata_inter,txwrsig,open,tx);
u5:TopLevel port map(clk,rst_n,fx,SmgDuan,SmgWei);
--确定下一次状态
process(rxwrsig,rst_n)
begin	
	if(rst_n = '0')then
		next_state <= s0;
	elsif(rising_edge(rxwrsig))then--收到数据
		if(rxdata_inter = b"1111_1111")then	
			next_state <= s1;
		elsif(rxdata_inter = b"0000_0000")then
			next_state <= s0;
		end if;
	end if;
end process;
process(uartclk)
begin	
	if(rising_edge(uartclk))then
		if(rst_n = '0')then
			current_state <= s0;
			send_state <= t0;
			uart_cnt <= 0;
			send_count <= 0;
			count <= 0;
			txwrsig <= '0';
		else
			case current_state is
				when s0 => current_state <= next_state;
				when s1 => case send_state is
									when t0=>if(send_count = 6000)then
													send_count <= 0;
													send_state <= t1;
												elsif(uart_cnt = 255)then
													uart_cnt <= 0;
													send_count <= send_count + 1;
													send_temp <= addata_inter;
													txdata_inter <= send_temp;
													txwrsig <= '1';
												else
													uart_cnt <= uart_cnt + 1;
													txwrsig <= '0';
												end if;
									when t1=>if(count = T10MS)then
													current_state <= next_state;
													count <= 0;
													send_state <= t0;
												else	
													count <= count + 1;
												end if;
								end case;
			end case;
		end if;
	end if;
end process;
end behavioral;